using API.Helpers;
using API.Models.DBModels;
using API.Repository.Generics;
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
            var userRepo = context.HttpContext.RequestServices.GetService<IGenericRepo<UserDBM>>();

            UserDBM user = null;

            bool isValid = false;

            if (context.HttpContext.Request.Headers.ContainsKey(jwtTokenHeaderKey))
            {
                context.HttpContext.Request.Headers.TryGetValue(jwtTokenHeaderKey, out StringValues jwtToken);
                isValid = AuthHelper.ValidateToken(jwtToken, configuration["Security:Issuer"], configuration["Security:SecretKey"], _role);

                user = GetUserByToken(userRepo, jwtToken);
            }

            if (user == null || !user.IsActive)
            {
                isValid = false;
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

        private UserDBM GetUserByToken(IGenericRepo<UserDBM> userRepo, string jwtToken)
        {
            var email = AuthHelper.GetEmailFromJwtToken(jwtToken);

            return userRepo.Get(c => c.Email == email).FirstOrDefault();
        }
    }
}
