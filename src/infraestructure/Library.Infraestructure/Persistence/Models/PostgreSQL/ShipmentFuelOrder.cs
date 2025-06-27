using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentFuelOrder
{
    public long Id { get; set; }

    public long ShipmentFreightId { get; set; }

    public long ShipmentFuelOrderTypeId { get; set; }

    public long ProviderTransportVehicleId { get; set; }

    public long FuelOrderIssuerId { get; set; }

    public long ShipmentGasStationId { get; set; }

    public long ProviderDriverId { get; set; }

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

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual BusinessProviderProfile FuelOrderIssuer { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual BusinessProviderDriver ProviderDriver { get; set; } = null!;

    public virtual BusinessProviderTransportVehicle ProviderTransportVehicle { get; set; } = null!;

    public virtual ShipmentFreight ShipmentFreight { get; set; } = null!;

    public virtual ShipmentFuelOrderType ShipmentFuelOrderType { get; set; } = null!;

    public virtual ShipmentGasStation ShipmentGasStation { get; set; } = null!;
}
