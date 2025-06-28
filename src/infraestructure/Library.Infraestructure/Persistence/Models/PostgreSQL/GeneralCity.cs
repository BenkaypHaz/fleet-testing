using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class GeneralCity
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long RegionId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BusinessPartnerProviderProfile> BusinessPartnerProviderProfiles { get; set; } = new List<BusinessPartnerProviderProfile>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CustomerProfile> CustomerProfiles { get; set; } = new List<CustomerProfile>();

    public virtual ICollection<CustomerWarehouse> CustomerWarehouses { get; set; } = new List<CustomerWarehouse>();

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual GeneralRegion Region { get; set; } = null!;

    public virtual ICollection<SettingDispatchBranch> SettingDispatchBranches { get; set; } = new List<SettingDispatchBranch>();

    public virtual ICollection<ShipmentGasStation> ShipmentGasStations { get; set; } = new List<ShipmentGasStation>();
}
