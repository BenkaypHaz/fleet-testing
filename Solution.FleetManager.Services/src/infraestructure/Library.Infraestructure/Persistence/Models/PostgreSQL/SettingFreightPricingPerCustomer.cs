using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class SettingFreightPricingPerCustomer
{
    public long Id { get; set; }

    public long SettingDispatchBranchId { get; set; }

    public long CustomerWarehouseId { get; set; }

    public decimal Price { get; set; }

    public decimal Cost { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual CustomerWarehouse CustomerWarehouse { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual SettingDispatchBranch SettingDispatchBranch { get; set; } = null!;
}
