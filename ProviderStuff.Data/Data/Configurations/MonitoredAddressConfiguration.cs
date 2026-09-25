using ProviderStuff.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProviderStuff.Data.Data.Configurations;

public class MonitoredAddressConfiguration : IEntityTypeConfiguration<MonitoredAddress>
{
    public void Configure(EntityTypeBuilder<MonitoredAddress> builder)
    {
        builder.HasKey(ma => ma.Id);

        builder.HasMany(ma => ma.PingTestRuns)
            .WithOne(t => t.MonitoredAddress)
            .HasForeignKey(t => t.MonitoredAddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ma => ma.StatusChangeLogs)
            .WithOne(scl => scl.MonitoredAddress)
            .HasForeignKey(scl => scl.MonitoredAddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ma => ma.IpAddress)
            .IsRequired()
            .HasMaxLength(30);
    }
}
