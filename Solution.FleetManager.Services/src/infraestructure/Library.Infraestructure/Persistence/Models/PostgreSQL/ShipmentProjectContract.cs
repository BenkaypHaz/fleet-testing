using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentProjectContract
{
    public long Id { get; set; }

    public long CustomerProfileId { get; set; }

    public long SettingDispatchBranchId { get; set; }

    public long ContractNumber { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int ExpectedFreight { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual SettingDispatchBranch SettingDispatchBranch { get; set; } = null!;

    public virtual ICollection<ShipmentFreight> ShipmentFreights { get; set; } = new List<ShipmentFreight>();
}
