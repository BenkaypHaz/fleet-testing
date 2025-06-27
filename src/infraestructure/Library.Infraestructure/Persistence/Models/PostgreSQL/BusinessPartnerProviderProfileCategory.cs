using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessPartnerProviderProfileCategory
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<BusinessPartnerProviderProfile> BusinessPartnerProviderProfiles { get; set; } = new List<BusinessPartnerProviderProfile>();
}
