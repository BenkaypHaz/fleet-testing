using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessPartnerPositionType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<BusinessPartnerProviderProfileContactPerson> BusinessPartnerProviderProfileContactPeople { get; set; } = new List<BusinessPartnerProviderProfileContactPerson>();
}
