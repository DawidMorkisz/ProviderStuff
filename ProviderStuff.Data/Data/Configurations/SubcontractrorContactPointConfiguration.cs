using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Data.Data.Configurations;

public class SubcontractorContactPointConfiguration : IEntityTypeConfiguration<SubcontractorContactPoint>
{
    public void Configure(EntityTypeBuilder<SubcontractorContactPoint> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.Value)
            .IsRequired()
            .HasMaxLength(200);
    }
}