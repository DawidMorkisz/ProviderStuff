namespace ProviderStuff.Domain.Entities;

public class MonitoredAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public required string IpAddress { get; set; }

    public bool IsPublic { get; set; } = false;

    public int PingIntervalSeconds { get; set; } = 5;

    public bool IsActive { get; set; } = true;

    public ICollection<PingTestRun> PingTestRuns { get; set; } = [];

    public ICollection<StatusChangeLog> StatusChangeLogs { get; set; } = [];

    public Guid? SubcontractorId { get; set; }

    public Subcontractor? Subcontractor { get; set; }
}