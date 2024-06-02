namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.ActivateEventEndpoint;

using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

public class ActivateEventEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<ActivateEventRequest>
    .WithoutResponse
{
    [HttpPost("/events/activate-event")]
    public override async Task<ActionResult> HandleAsync([FromBody] ActivateEventRequest request)
    {
        var cmdResult = CreatorActivatesAnEventCommand.Create(request.EventId);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
}

public record ActivateEventRequest(string EventId);