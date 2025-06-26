using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthUserRole
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long RoleId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual AuthRole Role { get; set; } = null!;

    public virtual AuthUser User { get; set; } = null!;
}
