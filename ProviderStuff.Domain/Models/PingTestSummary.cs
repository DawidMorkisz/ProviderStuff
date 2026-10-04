namespace ProviderStuff.Domain.Models;

public record PingTestSummary
{
    public required int TotalPings { get; init; }
    public required int SuccessCount { get; init; }
    public required int PacketLost { get; init; }
    public required double PacketLossPercent { get; init; }
    public required double AverageResponseTimeMs { get; init; }
    public required int MinResponseTimeMs { get; init; }
    public required int MaxResponseTimeMs { get; init; }
    public required List<PingOutcome> RawResults { get; init; }
}