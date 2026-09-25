using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Data.Data.Configurations;

    public class SubcontractorConfiguration : IEntityTypeConfiguration<Subcontractor>
{
    public void Configure(EntityTypeBuilder<Subcontractor> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(300);

        builder.HasMany(s => s.ContactPoints)
            .WithOne(cp => cp.Subcontractor)
            .HasForeignKey(cp => cp.SubcontractorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.MonitoredAddresses)
            .WithOne(ma => ma.Subcontractor)
            .HasForeignKey(ma => ma.SubcontractorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
