using ProviderStuff.Domain.Entities;
using ProviderStuff.Domain.Models;

namespace ProviderStuff.Domain.Interfaces.Services;

public interface IStatusEvaluatorService
{
    Task<Status> EvaluateStatusAsync(IEnumerable<PingOutcome> pingOutcomes, CancellationToken cancellationToken = default);
}