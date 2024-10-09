using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateEventDescriptionEndpoint;

public class UpdateEventDescriptionEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<UpdateEventDescriptionRequest>
    .WithoutResponse
{
    [HttpPost("events/update-event-description")]
    public override async Task<ActionResult> HandleAsync([FromBody] UpdateEventDescriptionRequest request)
    {
        var cmdResult = UpdateEventDescriptionCommand.Create(request.EventId, request.NewDescription);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record UpdateEventDescriptionRequest(string EventId, string NewDescription);