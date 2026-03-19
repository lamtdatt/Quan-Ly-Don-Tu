using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DANGCAPNE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? Array.Empty<string>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;
            var userId = http.Session.GetInt32("UserId");
            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (_roles.Length == 0)
            {
                base.OnActionExecuting(context);
                return;
            }

            var roles = (http.Session.GetString("Roles") ?? string.Empty)
                .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (!_roles.Any(r => roles.Contains(r)))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
