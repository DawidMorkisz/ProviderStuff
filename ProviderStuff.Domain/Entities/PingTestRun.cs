namespace ProviderStuff.Domain.Entities;

public class PingTestRun
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MonitoredAddressId { get; set; }
    public MonitoredAddress MonitoredAddress { get; set; } = null!;

    public DateTime RunAt { get; set; } = DateTime.UtcNow;
    public int TotalPings { get; set; }
    public int SuccessCount { get; set; }
    public double PacketLossPercent { get; set; }
    public double AverageResponseTimeMs { get; set; }
    public int MinResponseTimeMs { get; set; }
    public int MaxResponseTimeMs { get; set; }

    public ICollection<PingResult> PingResults { get; set; } = [];
}