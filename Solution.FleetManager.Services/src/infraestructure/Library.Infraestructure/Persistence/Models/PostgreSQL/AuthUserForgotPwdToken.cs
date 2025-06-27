using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthUserForgotPwdToken
{
    public long Id { get; set; }

    public string Token { get; set; } = null!;

    public long UserId { get; set; }

    public DateTime ExpirationDate { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser? CreatedByNavigation { get; set; }

    public virtual AuthUser User { get; set; } = null!;
}
