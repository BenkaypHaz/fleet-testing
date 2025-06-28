using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AccountingExpense
{
    public long Id { get; set; }

    public long ShipmentFreightId { get; set; }

    public long AccountingExpenseTypeId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AccountingExpenseType AccountingExpenseType { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ShipmentFreight ShipmentFreight { get; set; } = null!;
}
