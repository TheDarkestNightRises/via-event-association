using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.MakeEventPublicEndpoint;

public class MakeEventPublicEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<MakeEventPublicRequest>
    .WithoutResponse
{
    [HttpPost("events/public-event")]
    public override async Task<ActionResult> HandleAsync([FromBody] MakeEventPublicRequest request)
    {
        var cmdResult = MakeEventPublicCommand.Create(request.EventId);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record MakeEventPublicRequest(string EventId);