using ProviderStuff.Domain.Entities;
using ProviderStuff.Domain.Interfaces.Services;
using ProviderStuff.Domain.Models;

namespace ProviderStuff.Domain.Services;

public class StatusEvaluator : IStatusEvaluatorService
{
    private const int FailureThreshold = 5;

    public Task<Status> EvaluateStatusAsync(IEnumerable<PingOutcome> pingOutcomes, CancellationToken cancellationToken = default)
    {
        var outcomes = pingOutcomes?.ToList();

        if (outcomes is null || outcomes.Count == 0)
        {
            throw new ArgumentException("Ping outcomes collection cannot be null or empty.", nameof(pingOutcomes));
        }

        var status = outcomes.TakeLast(FailureThreshold).All(po => !po.IsSuccess)
            ? Status.Offline
            : Status.Online;

        return Task.FromResult(status);
    }
}
