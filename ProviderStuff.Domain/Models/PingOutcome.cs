namespace ProviderStuff.Domain.Models;

public record PingOutcome
{
    public required bool IsSuccess { get; init; }

    public int ResponseTimeMs { get; init; }

    public string? ErrorMessage { get; init; }
}
