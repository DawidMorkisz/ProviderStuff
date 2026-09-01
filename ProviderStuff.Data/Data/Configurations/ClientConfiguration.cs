using Microsoft.EntityFrameworkCore;
using ProviderStuff.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProviderStuff.Data.Data.Configuratons;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.MonitoredAddresses)
            .WithOne(ma => ma.Client)
            .HasForeignKey(ma => ma.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.ContactPoints)
            .WithOne(cp => cp.Client)
            .HasForeignKey(cp => cp.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
