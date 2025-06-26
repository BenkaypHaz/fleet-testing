using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessProviderProfile
{
    public long Id { get; set; }

    public long BusinessProviderProfileTypeId { get; set; }

    public long GeneralCityId { get; set; }

    public string LegalId { get; set; } = null!;

    public string BusinessName { get; set; } = null!;

    public string? LegalName { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BusinessProviderDriver> BusinessProviderDrivers { get; set; } = new List<BusinessProviderDriver>();

    public virtual ICollection<BusinessProviderProfileContactPerson> BusinessProviderProfileContactPeople { get; set; } = new List<BusinessProviderProfileContactPerson>();

    public virtual BusinessProviderProfileType BusinessProviderProfileType { get; set; } = null!;

    public virtual ICollection<BusinessProviderTransportVehicle> BusinessProviderTransportVehicles { get; set; } = new List<BusinessProviderTransportVehicle>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual GeneralCity GeneralCity { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<ShipmentFuelOrder> ShipmentFuelOrders { get; set; } = new List<ShipmentFuelOrder>();
}
