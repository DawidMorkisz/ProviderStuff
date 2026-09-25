using System.Net.NetworkInformation;
using ProviderStuff.Domain.Interfaces.Services;
using ProviderStuff.Domain.Models;

namespace ProviderStuff.Domain.Services;

public class PingService : IPingService
{
    public async Task<PingOutcome> PingAsync(string ipAddress, int timeoutMs, CancellationToken cancellationToken = default)
    {
        using var ping = new Ping();

        try
        {
            var reply = await ping.SendPingAsync(ipAddress, timeoutMs);

            return new PingOutcome
            {
                IsSuccess = reply.Status == IPStatus.Success,
                ResponseTimeMs = reply.Status == IPStatus.Success ? (int)reply.RoundtripTime : 0,
                ErrorMessage = reply.Status != IPStatus.Success ? reply.Status.ToString() : null
            };
        }
        catch (PingException ex)
        {
            return new PingOutcome
            {
                IsSuccess = false,
                ResponseTimeMs = 0,
                ErrorMessage = ex.InnerException?.Message ?? ex.Message
            };
        }
    }
}