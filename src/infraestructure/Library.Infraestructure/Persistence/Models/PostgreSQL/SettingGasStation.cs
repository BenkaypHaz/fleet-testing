using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class SettingGasStation
{
    public long Id { get; set; }

    public long GeneralCityId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountingFuelOrder> AccountingFuelOrders { get; set; } = new List<AccountingFuelOrder>();

    public virtual GeneralCity GeneralCity { get; set; } = null!;
}
