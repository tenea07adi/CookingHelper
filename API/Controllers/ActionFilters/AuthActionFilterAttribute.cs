using Core.Entities.Persisted;
using Core.Ports.Driving;
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
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
            var jwtTokenService = context.HttpContext.RequestServices.GetService<IJwtTokenService>();

            User? user = null;

            bool isValid = false;

            if (context.HttpContext.Request.Headers.ContainsKey(jwtTokenHeaderKey))
            {
                context.HttpContext.Request.Headers.TryGetValue(jwtTokenHeaderKey, out StringValues jwtToken);
                isValid = jwtTokenService!.ValidateToken(jwtToken, configuration["Security:Issuer"], configuration["Security:SecretKey"], _role);

                user = GetUserByToken(jwtTokenService, authService!, jwtToken!);
            }

            if (user == null || !user.IsActive)
            {
                isValid = false;
            }

            if (isValid)
            {
                PopulateSessionInfo(context, user!);
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

        private User? GetUserByToken(IJwtTokenService jwtTokenService, IAuthService authService, string jwtToken)
        {
            var email = jwtTokenService.GetEmailFromJwtToken(jwtToken);

            return authService.GetUserByEmail(email);
        }

        private void PopulateSessionInfo(ActionExecutingContext context, User user)
        {
            var sessionInfoService = context.HttpContext.RequestServices.GetService<ISessionInfoService>();

            sessionInfoService!.SetCurrentUserInfo(user);
        }
    }
}
