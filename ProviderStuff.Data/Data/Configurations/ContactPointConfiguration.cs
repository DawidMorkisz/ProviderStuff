using ProviderStuff.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProviderStuff.Data.Data.Configurations;

public class ContactPointConfiguration : IEntityTypeConfiguration<ContactPoint>
{
    public void Configure(EntityTypeBuilder<ContactPoint> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(cp => cp.Type)
            .HasConversion<string>()
            .HasMaxLength(10);
    }
    
}
