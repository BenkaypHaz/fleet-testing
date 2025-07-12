using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentGpsDevice
{
    public long Id { get; set; }

    public long? CustomerProfileId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual CustomerProfile? CustomerProfile { get; set; }

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<ShipmentProjectContractTransportVehicle> ShipmentProjectContractTransportVehicles { get; set; } = new List<ShipmentProjectContractTransportVehicle>();
}
