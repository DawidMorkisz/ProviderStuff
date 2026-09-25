using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderStuff.Domain.Constants;

namespace ProviderStuff.Domain.Entities;

public class SubcontractorContactPoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SubcontractorId { get; set; }

    public Subcontractor Subcontractor { get; set; } = null!;

    public ContactType Type { get; set; }

    public required string Value { get; set; }

}
