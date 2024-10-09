using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Filters;

public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        context.Result = new ObjectResult(exception.Message)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}