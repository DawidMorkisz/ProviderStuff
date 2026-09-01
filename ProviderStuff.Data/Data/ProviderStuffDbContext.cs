using Microsoft.EntityFrameworkCore;
using ProviderStuff.Data.Data.Configurations;
using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Data.Data;

public class ProviderStuffDbContext(DbContextOptions<ProviderStuffDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }

    public DbSet<StatusChangeLog> StatusChangeLogs { get; set; }

    public DbSet<MonitoredAddress> MonitoredAddresses { get; set; }

    public DbSet<PingResult> PingResults { get; set; }

    public DbSet<ContactPoint> ContactPoints { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProviderStuffDbContext).Assembly);
    }
}