using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Common.Filters.Authorization
{
    public class AuthorizeAdministratorsFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _requiredRoles;

        /// <inheritdoc/>
        public AuthorizeAdministratorsFilter(params string[] requiredRoles)
        {
            _requiredRoles = requiredRoles;
        }

        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity != null && !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var hasRequiredRole = _requiredRoles.Any(role => context.HttpContext.User.IsInRole(role));
            if (!hasRequiredRole)
                context.Result = new ForbidResult();
        }
    }
}
