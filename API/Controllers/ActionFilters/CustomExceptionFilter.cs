using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ActionFilters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new { message = context.Exception.Message };

            context.Result = context.Exception switch
            {
                ArgumentException => new BadRequestObjectResult(response),
                InvalidOperationException => new BadRequestObjectResult(response),
                KeyNotFoundException => new NotFoundObjectResult(response),
                UnauthorizedAccessException => new UnauthorizedObjectResult(response),
                NotImplementedException => new ObjectResult(response) { StatusCode = 501 },
                _ => new ObjectResult(response) { StatusCode = 500 }
            };
        }
    }
}
