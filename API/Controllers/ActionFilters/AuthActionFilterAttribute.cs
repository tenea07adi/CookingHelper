using API.Helpers;
using API.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace API.Controllers.ActionFilters
{
    public class AuthActionFilterAttribute : ActionFilterAttribute
    {
        private Roles _role;

        public AuthActionFilterAttribute() : this(Roles.ReadOnly)
        {

        }

        public AuthActionFilterAttribute(Roles role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var jwtTokenHeaderKey = "AuthToken";

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            bool isValid = false;

            if (context.HttpContext.Request.Headers.ContainsKey(jwtTokenHeaderKey))
            {
                context.HttpContext.Request.Headers.TryGetValue(jwtTokenHeaderKey, out StringValues jwtToken);
                isValid = AuthHelper.ValidateToken(jwtToken, configuration["Security:Issuer"], configuration["Security:SecretKey"], _role);
            }

            if (isValid)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new UnauthorizedObjectResult("Unauthorized!");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
