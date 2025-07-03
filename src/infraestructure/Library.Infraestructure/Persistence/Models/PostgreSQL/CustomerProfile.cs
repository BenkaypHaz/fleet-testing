using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class CustomerProfile
{
    public long Id { get; set; }

    public long GeneralCityId { get; set; }

    public string LegalId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? LegalName { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CustomerContactPerson> CustomerContactPeople { get; set; } = new List<CustomerContactPerson>();

    public virtual ICollection<CustomerWarehouse> CustomerWarehouses { get; set; } = new List<CustomerWarehouse>();

    public virtual GeneralCity GeneralCity { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<ShipmentGpsDevice> ShipmentGpsDevices { get; set; } = new List<ShipmentGpsDevice>();

    public virtual ICollection<ShipmentProjectContract> ShipmentProjectContracts { get; set; } = new List<ShipmentProjectContract>();
}
