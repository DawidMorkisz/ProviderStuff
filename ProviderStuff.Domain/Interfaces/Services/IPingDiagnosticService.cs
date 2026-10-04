using ProviderStuff.Domain.Models;

namespace ProviderStuff.Domain.Interfaces.Services;

public interface IPingDiagnosticService
{
    Task<PingTestSummary> RunTestAsync(string ipAddress, int pingCount, int timeoutMs, CancellationToken cancellationToken = default);
}
