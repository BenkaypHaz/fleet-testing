using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class GeneralCountry
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public string? IsoCode { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<GeneralRegion> GeneralRegions { get; set; } = new List<GeneralRegion>();

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
