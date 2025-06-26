using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthAuthorization
{
    public long Id { get; set; }

    public string? Description { get; set; }

    public string? RouteValue { get; set; }

    public long? ModuleId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AuthRoleAuthorization> AuthRoleAuthorizations { get; set; } = new List<AuthRoleAuthorization>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual AuthModule? Module { get; set; }
}
