using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentExpenseType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<ShipmentExpense> ShipmentExpenses { get; set; } = new List<ShipmentExpense>();
}
