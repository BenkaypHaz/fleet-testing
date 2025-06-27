using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthRole
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AuthRoleAuthorization> AuthRoleAuthorizations { get; set; } = new List<AuthRoleAuthorization>();

    public virtual ICollection<AuthUserRole> AuthUserRoles { get; set; } = new List<AuthUserRole>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

}
