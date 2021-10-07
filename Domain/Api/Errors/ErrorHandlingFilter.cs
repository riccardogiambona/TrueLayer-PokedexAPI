using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Api.Errors
{
    /// <summary>
    /// This is the class that transforms every raised exception in a common error dto
    /// </summary>
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(ApiExceptionConverter.ConvertToDto(context.Exception));
            context.ExceptionHandled = true;
        }
    }
}
