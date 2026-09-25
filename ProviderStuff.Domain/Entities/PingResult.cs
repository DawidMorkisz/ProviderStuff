namespace ProviderStuff.Domain.Entities;

public class PingResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PingTestRunId { get; set; }
    public PingTestRun PingTestRun { get; set; } = null!;

    public bool IsSuccess { get; set; }
    public int ResponseTimeMs { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}