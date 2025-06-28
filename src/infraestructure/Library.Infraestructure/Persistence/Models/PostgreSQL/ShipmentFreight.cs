using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentFreight
{
    public long Id { get; set; }

    public long ShipmentProjectContractId { get; set; }

    public long BusinessPartnerProviderTransportVehicleId { get; set; }

    public long CustomerWarehouseId { get; set; }

    public long BusinessPartnerProviderDriverId { get; set; }

    public long ShipmentFreightStatusId { get; set; }

    public long ShipmentFreightTypeId { get; set; }

    public long FreightProductTypeId { get; set; }

    public decimal Price { get; set; }

    public decimal Cost { get; set; }

    public string? Observations { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountingExpense> AccountingExpenses { get; set; } = new List<AccountingExpense>();

    public virtual BusinessPartnerProviderDriver BusinessPartnerProviderDriver { get; set; } = null!;

    public virtual BusinessPartnerProviderTransportVehicle BusinessPartnerProviderTransportVehicle { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual CustomerWarehouse CustomerWarehouse { get; set; } = null!;

    public virtual FreightProductType FreightProductType { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ShipmentFreightStatus ShipmentFreightStatus { get; set; } = null!;

    public virtual ICollection<ShipmentFreightStatusLog> ShipmentFreightStatusLogs { get; set; } = new List<ShipmentFreightStatusLog>();

    public virtual ShipmentFreightType ShipmentFreightType { get; set; } = null!;

    public virtual ShipmentProjectContract ShipmentProjectContract { get; set; } = null!;
}
