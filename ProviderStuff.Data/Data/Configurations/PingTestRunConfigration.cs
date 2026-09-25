using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Data.Data.Configurations;

public class PingTestRunConfiguration : IEntityTypeConfiguration<PingTestRun>
{
    public void Configure(EntityTypeBuilder<PingTestRun> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(t => t.PingResults)
            .WithOne(r => r.PingTestRun)
            .HasForeignKey(r => r.PingTestRunId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => new { t.MonitoredAddressId, t.RunAt });
    }
}