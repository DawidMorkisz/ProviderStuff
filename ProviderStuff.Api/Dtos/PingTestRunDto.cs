using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Api.Dtos;

public record PingTestRunDto
{
    public required Guid Id { get; init; }
    public required Guid MonitoredAddressId { get; init; }
    public required DateTime RunAt { get; init; }
    public required int TotalPings { get; init; }
    public required int SuccessCount { get; init; }
    public required int PacketLost { get; init; }
    public required double PacketLossPercent { get; init; }
    public required double AverageResponseTimeMs { get; init; }
    public required int MinResponseTimeMs { get; init; }
    public required int MaxResponseTimeMs { get; init; }
    public required List<PingResultDto> PingResults { get; init; }
}