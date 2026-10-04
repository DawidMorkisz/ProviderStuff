using ProviderStuff.Domain.Interfaces.Services;
using ProviderStuff.Domain.Models;

namespace ProviderStuff.Domain.Services;

public class PingDiagnosticService : IPingDiagnosticService
{
    private readonly IPingService _pingService;

    public PingDiagnosticService(IPingService pingService)
    {
        _pingService = pingService;
    }

    public async Task<PingTestSummary> RunTestAsync(string ipAddress, int pingCount, int timeoutMs, CancellationToken cancellationToken = default)
    {
        var results = new List<PingOutcome>(pingCount);

        for (var i = 0; i < pingCount; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            results.Add(await _pingService.PingAsync(ipAddress, timeoutMs, cancellationToken));
        }

        var successful = results.Where(r => r.IsSuccess).ToList();

        return new PingTestSummary
        {
            TotalPings = pingCount,
            SuccessCount = successful.Count,
            PacketLost = pingCount - successful.Count,
            PacketLossPercent = (pingCount - successful.Count) / (double)pingCount * 100,
            AverageResponseTimeMs = successful.Count > 0 ? successful.Average(r => r.ResponseTimeMs) : 0,
            MinResponseTimeMs = successful.Count > 0 ? successful.Min(r => r.ResponseTimeMs) : 0,
            MaxResponseTimeMs = successful.Count > 0 ? successful.Max(r => r.ResponseTimeMs) : 0,
            RawResults = results,
        };
    }
}