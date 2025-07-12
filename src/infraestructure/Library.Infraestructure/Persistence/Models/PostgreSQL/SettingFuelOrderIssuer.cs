using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class SettingFuelOrderIssuer
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PercentageOverprice { get; set; }

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AccountingFuelOrder> AccountingFuelOrders { get; set; } = new List<AccountingFuelOrder>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<ShipmentProjectContract> ShipmentProjectContracts { get; set; } = new List<ShipmentProjectContract>();
}
