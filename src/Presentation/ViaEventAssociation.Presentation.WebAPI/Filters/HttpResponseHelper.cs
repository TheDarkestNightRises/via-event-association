using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Filters;

public static class HttpResponseHelper
{
    public static ActionResult ToResponse<T, TResponse>(this Result<T> result, Func<T, TResponse> successMapper)
    {
        if (result.IsSuccess)
        {
            var response = successMapper(result.PayLoad);
            return new OkObjectResult(response);
        }
        return new BadRequestObjectResult(result.Errors);
    }
    
    public static ActionResult ToResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new NoContentResult();
        }
        return new BadRequestObjectResult(result.Errors);
    }
}