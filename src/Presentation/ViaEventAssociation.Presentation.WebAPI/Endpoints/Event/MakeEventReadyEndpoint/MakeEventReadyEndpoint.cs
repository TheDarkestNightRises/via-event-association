using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.MakeEventReadyEndpoint;

public class MakeEventReadyEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<MakeEventReadyRequest>
    .WithoutResponse
{

    [HttpPost("events/make-ready")]
    public override async Task<ActionResult> HandleAsync([FromBody] MakeEventReadyRequest request)
    {
        var cmdResult = MakeEventReadyCommand.Create(request.Id);
        if (cmdResult.IsFailure)
        {
            return BadRequest(cmdResult.Errors);
        }
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record MakeEventReadyRequest(string Id);
