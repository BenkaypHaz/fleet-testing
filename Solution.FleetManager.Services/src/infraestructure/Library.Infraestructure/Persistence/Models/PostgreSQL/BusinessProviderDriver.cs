using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessProviderDriver
{
    public long Id { get; set; }

    public long? BusinessProviderProfileId { get; set; }

    public string? NationalId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string Nationality { get; set; } = null!;

    public DateOnly? HireDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual BusinessProviderProfile? BusinessProviderProfile { get; set; }

    public virtual ICollection<BusinessProviderTransportVehicle> BusinessProviderTransportVehicles { get; set; } = new List<BusinessProviderTransportVehicle>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<ShipmentFreight> ShipmentFreights { get; set; } = new List<ShipmentFreight>();

    public virtual ICollection<ShipmentFuelOrder> ShipmentFuelOrders { get; set; } = new List<ShipmentFuelOrder>();
}
