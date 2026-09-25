using ProviderStuff.Domain.Constants;

namespace ProviderStuff.Domain.Entities;

public class ContactPoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public ContactType Type { get; set; }

    public required string Value { get; set; }
}
