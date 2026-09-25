using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderStuff.Domain.Entities;

public class Subcontractor
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CompanyName { get; set; } = null!;

    public string? Address { get; set; }

    public ICollection<SubcontractorContactPoint> ContactPoints { get; set; } = [];
    
    public ICollection<MonitoredAddress> MonitoredAddresses { get; set; } = [];
}
