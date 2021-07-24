using Homuai.Communication.Error;
using Homuai.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Homuai.Api.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HomuaiException)
                HandleProjectException(context);
            else
                ThrowUnknowError(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
                ThrowBadRequestValidation(context);
            else if (context.Exception is NotFoundException)
                ThrowNotFound(context);
            else if (context.Exception is InvalidLoginException)
                ThrowUnauthorized(context);
            else
                ThrowBadRequest(context);
        }

        private void ThrowBadRequestValidation(ExceptionContext context)
        {
            ErrorOnValidationException validacaoException = (ErrorOnValidationException)context.Exception;

            context.Result = new BadRequestObjectResult(new ErrorJson(validacaoException.ErrorMensages));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        private void ThrowBadRequest(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ErrorJson(context.Exception.Message));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        private void ThrowUnauthorized(ExceptionContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorJson(context.Exception.Message));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        private void ThrowNotFound(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(new ErrorJson(context.Exception.Message));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        private void ThrowUnknowError(ExceptionContext context)
        {
            context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
