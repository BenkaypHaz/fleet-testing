using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class CustomerContactPerson
{
    public long Id { get; set; }

    public long CustomerProfileId { get; set; }

    public string? NationalId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
