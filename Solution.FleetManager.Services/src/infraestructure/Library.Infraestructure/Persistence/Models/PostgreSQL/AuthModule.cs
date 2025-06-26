using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthModule
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AuthAuthorization> AuthAuthorizations { get; set; } = new List<AuthAuthorization>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
