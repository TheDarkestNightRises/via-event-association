using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.GuestAcceptsInvitationEndpoint;

public class GuestAcceptsInvitationEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<GuestAcceptsInvitationRequest>
    .WithoutResponse
{
    [HttpPost("/events/guest-accepts-invitation")]
    public override async Task<ActionResult> HandleAsync(GuestAcceptsInvitationRequest request)
    {
        try
        {
            var cmdResult = GuestAcceptsInvitationCommand.Create(request.EventId, request.GuestId);
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

public record GuestAcceptsInvitationRequest(string EventId, string GuestId);