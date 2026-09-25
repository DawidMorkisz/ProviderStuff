using ProviderStuff.Domain.Models;

namespace ProviderStuff.Domain.Interfaces.Services;

public interface IPingService
{
    Task<PingOutcome> PingAsync(string ipAddress, int timeoutMs, CancellationToken cancellationToken = default);
}
