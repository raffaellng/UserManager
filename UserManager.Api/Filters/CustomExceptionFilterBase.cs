using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserManager.Api.Filters
{
    public class CustomExceptionFilterBase : IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is FluentValidation.ValidationException validationException)
            {
                var errors = validationException.Errors
                    .Select(errors => $"{errors.PropertyName}: {errors.ErrorMessage}")
                    .ToList();

                var result = new ObjectResult(new { Errors = errors })
                {
                    StatusCode = 400
                };

                exceptionContext.Result = result;
                exceptionContext.ExceptionHandled = true;
            }
            else if (
                exceptionContext.Exception is ArgumentNullException ||
                        exceptionContext.Exception is KeyNotFoundException)
            {
                exceptionContext.Result = new NotFoundObjectResult(new { Error = "Recurso não encontrado" });
                exceptionContext.ExceptionHandled = true;
            }
            else if (
                exceptionContext.Exception is HttpRequestException ||
                        exceptionContext.Exception is InvalidOperationException)
            {
                exceptionContext.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}