using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentProjectContractTransportVehicle
{
    public long Id { get; set; }

    public long ShipmentProjectContractId { get; set; }

    public long BusinessPartnerTransportVehicleId { get; set; }

    public long ShipmentRotulationTypeId { get; set; }

    public long? ShipmentGpsDeviceId { get; set; }

    public int RotulationNumber { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual BusinessPartnerProviderTransportVehicle BusinessPartnerTransportVehicle { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ShipmentGpsDevice? ShipmentGpsDevice { get; set; }

    public virtual ShipmentProjectContract ShipmentProjectContract { get; set; } = null!;

    public virtual ShipmentRotulationType ShipmentRotulationType { get; set; } = null!;
}
