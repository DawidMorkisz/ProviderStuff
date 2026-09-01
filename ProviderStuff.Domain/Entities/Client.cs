namespace ProviderStuff.Domain.Entities;

public class Client
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public required string LocationAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection <ContactPoint> ContactPoints { get; set; } = [];

    public ICollection<MonitoredAddress> MonitoredAddresses { get; set; } = [];
}
