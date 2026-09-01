using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Data.Data.Configurations;

public class PingResultConfiguration : IEntityTypeConfiguration<PingResult>
{
    public void Configure(EntityTypeBuilder<PingResult> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => new { p.MonitoredAddressId, p.Timestamp });
    }
}