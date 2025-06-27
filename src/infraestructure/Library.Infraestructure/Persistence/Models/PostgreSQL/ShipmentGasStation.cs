using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentGasStation
{
    public long Id { get; set; }

    public long GeneralCityId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual GeneralCity GeneralCity { get; set; } = null!;

    public virtual ICollection<ShipmentFuelOrder> ShipmentFuelOrders { get; set; } = new List<ShipmentFuelOrder>();
}
