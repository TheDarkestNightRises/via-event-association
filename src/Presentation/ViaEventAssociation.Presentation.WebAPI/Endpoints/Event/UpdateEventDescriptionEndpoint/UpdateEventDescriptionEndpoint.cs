using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateEventDescriptionEndpoint;

public class UpdateEventDescriptionEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<UpdateEventDescriptionRequest>
    .WithoutResponse
{
    
    [HttpPost("/events/update-event-description")]
    public override async Task<ActionResult> HandleAsync(UpdateEventDescriptionRequest request)
    {
        try
        {
            var cmdResult = UpdateEventDescriptionCommand.Create(request.EventId, request.NewDescription);
            if (cmdResult.IsFailure)
                return BadRequest(cmdResult.Errors);
            var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}
public record UpdateEventDescriptionRequest(string EventId, string NewDescription);