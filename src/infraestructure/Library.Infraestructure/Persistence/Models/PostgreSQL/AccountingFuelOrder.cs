using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AccountingFuelOrder
{
    public long Id { get; set; }

    public long AccountingFuelOrderTypeId { get; set; }

    public long SettingCurrencyId { get; set; }

    public long BusinessPartnerProviderTransportVehicleId { get; set; }

    public long BusinessPartnerFuelOrderIssuerId { get; set; }

    public long SettingGasStationId { get; set; }

    public long? BusinessPartnerProviderDriverId { get; set; }

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

    public virtual AccountingFuelOrderType AccountingFuelOrderType { get; set; } = null!;

    public virtual BusinessPartnerFuelOrderIssuer BusinessPartnerFuelOrderIssuer { get; set; } = null!;

    public virtual BusinessPartnerProviderDriver? BusinessPartnerProviderDriver { get; set; }

    public virtual BusinessPartnerProviderTransportVehicle BusinessPartnerProviderTransportVehicle { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual SettingCurrency SettingCurrency { get; set; } = null!;

    public virtual SettingGasStation SettingGasStation { get; set; } = null!;
}
