using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessPartnerFuelOrderIssuer
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PercentageOverprice { get; set; }

    public bool IsDefault { get; set; }

    public virtual ICollection<ShipmentFuelOrder> ShipmentFuelOrders { get; set; } = new List<ShipmentFuelOrder>();
}
