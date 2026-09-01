namespace ProviderStuff.Domain.Entities;

public class PingResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MonitoredAddressId { get; set; }

    public MonitoredAddress MonitoredAddress { get; set; } = null!;

    public bool IsSuccess { get; set; }

    public int ResponseTimeMs { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
