using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProviderStuff.Api.Dtos;
using ProviderStuff.Data.Data;
using ProviderStuff.Domain.Entities;
using ProviderStuff.Domain.Interfaces.Services;

namespace ProviderStuff.Api.Controllers;

[ApiController]
[Route("api/addresses/{addressId:guid}/[controller]")]
public class PingTestsController : ControllerBase
{
    private readonly IPingDiagnosticService _pingDiagnosticService;
    private readonly ProviderStuffDbContext _dbContext;

    public PingTestsController(IPingDiagnosticService pingDiagnosticService, ProviderStuffDbContext dbContext)
    {
        _pingDiagnosticService = pingDiagnosticService;
        _dbContext = dbContext;
    }

    [HttpPost("run")]
    public async Task<ActionResult<PingTestRunDto>> RunTest(
        Guid addressId,
        [FromQuery] int pingCount = 1000,
        [FromQuery] int timeoutMs = 2000,
        CancellationToken cancellationToken = default)
    {
        var address = await _dbContext.MonitoredAddresses
            .FirstOrDefaultAsync(a => a.Id == addressId, cancellationToken);

        if (address is null)
        {
            return NotFound();
        }

        var summary = await _pingDiagnosticService.RunTestAsync(address.IpAddress, pingCount, timeoutMs, cancellationToken);

        var testRun = new PingTestRun
        {
            MonitoredAddressId = address.Id,
            TotalPings = summary.TotalPings,
            SuccessCount = summary.SuccessCount,
            PacketLost = summary.PacketLost,
            PacketLossPercent = summary.PacketLossPercent,
            AverageResponseTimeMs = summary.AverageResponseTimeMs,
            MinResponseTimeMs = summary.MinResponseTimeMs,
            MaxResponseTimeMs = summary.MaxResponseTimeMs,
            PingResults = summary.RawResults.Select(r => new PingResult
            {
                IsSuccess = r.IsSuccess,
                ResponseTimeMs = r.ResponseTimeMs,
            }).ToList(),
        };

        _dbContext.PingTestRuns.Add(testRun);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToDto(testRun));
    }

    [HttpGet]
    public async Task<ActionResult<List<PingTestRunDto>>> GetTestRuns(Guid addressId, CancellationToken cancellationToken)
    {
        var testRuns = await _dbContext.PingTestRuns
            .Include(t => t.PingResults)
            .Where(t => t.MonitoredAddressId == addressId)
            .OrderByDescending(t => t.RunAt)
            .ToListAsync(cancellationToken);

        return Ok(testRuns.Select(ToDto).ToList());
    }

    private static PingTestRunDto ToDto(PingTestRun testRun) => new()
    {
        Id = testRun.Id,
        MonitoredAddressId = testRun.MonitoredAddressId,
        RunAt = testRun.RunAt,
        TotalPings = testRun.TotalPings,
        SuccessCount = testRun.SuccessCount,
        PacketLost = testRun.PacketLost,
        PacketLossPercent = testRun.PacketLossPercent,
        AverageResponseTimeMs = testRun.AverageResponseTimeMs,
        MinResponseTimeMs = testRun.MinResponseTimeMs,
        MaxResponseTimeMs = testRun.MaxResponseTimeMs,
        PingResults = testRun.PingResults.Select(r => new PingResultDto
        {
            IsSuccess = r.IsSuccess,
            ResponseTimeMs = r.ResponseTimeMs,
            Timestamp = r.Timestamp,
        }).ToList(),
    };
}