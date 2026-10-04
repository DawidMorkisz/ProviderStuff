using Microsoft.EntityFrameworkCore;
using ProviderStuff.Data.Data;
using ProviderStuff.Domain.Entities;
using ProviderStuff.Domain.Interfaces.Services;
using ProviderStuff.Domain.Models;

namespace ProviderStuff.Api.BackgroundServices;

public class PingWorkerBackgroundService : BackgroundService
{
    private const int HistorySize = 5;
    private const int MaxConcurrentPings = 50;
    private const int PingTimeoutMs = 2000;
    private static readonly TimeSpan CycleDelay = TimeSpan.FromSeconds(1);

    private readonly IPingService _pingService;
    private readonly IStatusEvaluatorService _statusEvaluator;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<PingWorkerBackgroundService> _logger;

    private readonly Dictionary<Guid, Queue<PingOutcome>> _recentOutcomes = new();
    private readonly Dictionary<Guid, Status> _lastKnownStatus = new();
    private readonly Dictionary<Guid, DateTime> _lastPingedAt = new();
    private readonly SemaphoreSlim _throttle = new(MaxConcurrentPings);

    private List<MonitoredAddress> _cachedAddresses = new();
    private DateTime _addressesCachedAt = DateTime.MinValue;
    private static readonly TimeSpan AddressCacheDuration = TimeSpan.FromSeconds(30);

    public PingWorkerBackgroundService(
        IPingService pingService,
        IStatusEvaluatorService statusEvaluator,
        IServiceScopeFactory scopeFactory,
        ILogger<PingWorkerBackgroundService> logger)
    {
        _pingService = pingService;
        _statusEvaluator = statusEvaluator;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await RunSingleCycleAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during ping cycle.");
            }

            await Task.Delay(CycleDelay, stoppingToken);
        }
    }

    private async Task RunSingleCycleAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProviderStuffDbContext>();

        if (DateTime.UtcNow - _addressesCachedAt > AddressCacheDuration)
        {
            _cachedAddresses = await dbContext.MonitoredAddresses
                .Where(ma => ma.IsActive)
                .ToListAsync(stoppingToken);

            _addressesCachedAt = DateTime.UtcNow;
        }

        var dueAddresses = _cachedAddresses.Where(IsDue).ToList();

        var tasks = dueAddresses.Select(address => ProcessAddressAsync(address, dbContext, stoppingToken));
        await Task.WhenAll(tasks);
    }

    private bool IsDue(MonitoredAddress address)
    {
        var lastPinged = _lastPingedAt.GetValueOrDefault(address.Id, DateTime.MinValue);
        var dueTime = lastPinged.AddSeconds(address.PingIntervalSeconds);

        return DateTime.UtcNow >= dueTime;
    }

    private async Task ProcessAddressAsync(MonitoredAddress address, ProviderStuffDbContext dbContext, CancellationToken stoppingToken)
    {
        await _throttle.WaitAsync(stoppingToken);

        try
        {
            var outcome = await _pingService.PingAsync(address.IpAddress, PingTimeoutMs, stoppingToken);
            _lastPingedAt[address.Id] = DateTime.UtcNow;

            var history = GetOrCreateHistory(address.Id);
            UpdateHistory(history, outcome);

            var newStatus = await _statusEvaluator.EvaluateStatusAsync(history, stoppingToken);
            var previousStatus = _lastKnownStatus.GetValueOrDefault(address.Id, Status.Online);

            if (newStatus != previousStatus)
            {
                dbContext.StatusChangeLogs.Add(new StatusChangeLog
                {
                    MonitoredAddressId = address.Id,
                    Status = newStatus,
                });

                await dbContext.SaveChangesAsync(stoppingToken);

                _logger.LogInformation(
                    "Status change detected for {IpAddress}: {PreviousStatus} -> {NewStatus}",
                    address.IpAddress, previousStatus, newStatus);

                // TODO: SignalR notification (Faza 5)
                // TODO: wysyłka e-mail/SMS (Faza 7)
            }

            _lastKnownStatus[address.Id] = newStatus;
        }
        finally
        {
            _throttle.Release();
        }
    }

    private Queue<PingOutcome> GetOrCreateHistory(Guid addressId)
    {
        if (!_recentOutcomes.TryGetValue(addressId, out var history))
        {
            history = new Queue<PingOutcome>(HistorySize);
            _recentOutcomes[addressId] = history;
        }

        return history;
    }

    private static void UpdateHistory(Queue<PingOutcome> history, PingOutcome outcome)
    {
        history.Enqueue(outcome);

        if (history.Count > HistorySize)
        {
            history.Dequeue();
        }
    }
}