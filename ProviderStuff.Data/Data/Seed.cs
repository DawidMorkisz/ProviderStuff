using Microsoft.EntityFrameworkCore;
using ProviderStuff.Data.Data;
using ProviderStuff.Domain.Constants;
using ProviderStuff.Domain.Entities;

namespace ProviderStuff.Data.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(ProviderStuffDbContext dbContext)
    {
        if (await dbContext.Clients.AnyAsync())
        {
            return;
        }

        var client = new Client
        {
            Name = "Testowy Klient Sp. z o.o.",
            LocationAddress = "ul. Testowa 1, 40-001 Katowice",
        };

        client.ContactPoints.Add(new ContactPoint
        {
            ClientId = client.Id,
            Type = ContactType.Email,
            Value = "kontakt@testowyklient.pl",
        });

        client.ContactPoints.Add(new ContactPoint
        {
            ClientId = client.Id,
            Type = ContactType.Phone,
            Value = "+48 123 456 789",
        });

        var addresses = new List<MonitoredAddress>
        {
            new()
            {
                ClientId = client.Id,
                IpAddress = "8.8.8.8",
                IsPublic = true,
                PingIntervalSeconds = 10,
                IsActive = true,
            },
            new()
            {
                ClientId = client.Id,
                IpAddress = "1.1.1.1",
                IsPublic = true,
                PingIntervalSeconds = 15,
                IsActive = true,
            },
            new()
            {
                ClientId = client.Id,
                IpAddress = "203.0.113.1", // reserved IP for documentation, should be unreachable
                IsPublic = true,
                PingIntervalSeconds = 10,
                IsActive = true,
            },
        };

        client.MonitoredAddresses = addresses;

        dbContext.Clients.Add(client);
        await dbContext.SaveChangesAsync();
    }
}