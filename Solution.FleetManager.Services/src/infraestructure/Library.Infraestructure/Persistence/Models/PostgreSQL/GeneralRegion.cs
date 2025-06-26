using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class GeneralRegion
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long CountryId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual GeneralCountry Country { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<GeneralCity> GeneralCities { get; set; } = new List<GeneralCity>();

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
