using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.GuestDeclinesInvitationEndpoint;

public class GuestDeclinesInvitationEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<GuestDeclinesInvitationEndpoint.GuestDeclinesInvitationRequest>
    .WithoutResponse
{
    
    [HttpPost("/events/guest-declines-invitation")]
    public override async Task<ActionResult> HandleAsync(GuestDeclinesInvitationRequest request)
    {
        try
        {
            var cmdResult = GuestDeclinesInvitationCommand.Create(request.EventId, request.GuestId);
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
    
    public record GuestDeclinesInvitationRequest(string EventId, string GuestId);

}