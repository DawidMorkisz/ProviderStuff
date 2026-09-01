namespace ProviderStuff.Domain.Entities;

public class StatusChangeLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid MonitoredAddressId { get; set; }

    public MonitoredAddress MonitoredAddress { get; set; } = null!;

    public Status Status { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}

public enum Status
{
    Online,
    Offline
}