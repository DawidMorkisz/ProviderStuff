using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderStuff.Api.Dtos;

public record PingResultDto
{
    public required bool IsSuccess { get; init; }
    public required int ResponseTimeMs { get; init; }
    public required DateTime Timestamp { get; init; }
}