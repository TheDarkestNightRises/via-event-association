using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.ParticipateInPublicEventEndpoint;

public class ParticipateInPublicEventEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<ParticipateInPublicEventRequest>
    .WithoutResponse
{
    
    [HttpPost("/events/participate-event")]
    public override async Task<ActionResult> HandleAsync(ParticipateInPublicEventRequest request)
    {
        try
        {
            var cmdResult = ParticipateInPublicEventCommand.Create(request.EventId, request.GuestId);
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
    
public record ParticipateInPublicEventRequest(string EventId, string GuestId);

