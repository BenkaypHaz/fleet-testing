using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentFuelOrder
{
    public long Id { get; set; }

    public long ShipmentFreightId { get; set; }

    public long ShipmentFuelOrderTypeId { get; set; }

    public long BusinessPartnerProviderTransportVehicleId { get; set; }

    public long BusinessPartnerFuelOrderIssuerId { get; set; }

    public long ShipmentGasStationId { get; set; }

    public long BusinessPartnerProviderDriverId { get; set; }

    public decimal QuantityGallon { get; set; }

    public decimal CostPerGallon { get; set; }

    public decimal TotalCost { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime IssuedDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual BusinessPartnerFuelOrderIssuer BusinessPartnerFuelOrderIssuer { get; set; } = null!;

    public virtual BusinessPartnerProviderDriver BusinessPartnerProviderDriver { get; set; } = null!;

    public virtual BusinessPartnerProviderTransportVehicle BusinessPartnerProviderTransportVehicle { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ShipmentFreight ShipmentFreight { get; set; } = null!;

    public virtual ShipmentFuelOrderType ShipmentFuelOrderType { get; set; } = null!;

    public virtual ShipmentGasStation ShipmentGasStation { get; set; } = null!;
}
