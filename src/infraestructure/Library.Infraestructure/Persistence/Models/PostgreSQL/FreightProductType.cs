using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class FreightProductType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ShipmentFreight> ShipmentFreights { get; set; } = new List<ShipmentFreight>();
}
