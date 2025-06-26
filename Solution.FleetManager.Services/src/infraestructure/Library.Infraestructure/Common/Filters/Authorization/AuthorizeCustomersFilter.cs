using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infraestructure.Common.Filters.Authorization
{
    public class AuthorizeCustomersFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _requiredRoles;

        /// <inheritdoc/>
        public AuthorizeCustomersFilter(params string[] requiredRoles)
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
