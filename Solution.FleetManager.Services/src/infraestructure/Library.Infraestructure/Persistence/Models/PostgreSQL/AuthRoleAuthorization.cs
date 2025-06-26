using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthRoleAuthorization
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public long AuthId { get; set; }

    public bool Read { get; set; }

    public bool Create { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthAuthorization Auth { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual AuthRole Role { get; set; } = null!;
}
