using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AccountingFuelOrderType
{
    public long Id { get; set; }

    public string SerialCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal CostPerGallon { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountingFuelOrder> AccountingFuelOrders { get; set; } = new List<AccountingFuelOrder>();

    public virtual ICollection<AccountingFuelPriceChangeHistory> AccountingFuelPriceChangeHistories { get; set; } = new List<AccountingFuelPriceChangeHistory>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
