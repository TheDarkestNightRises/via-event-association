using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.GuestAcceptsInvitationEndpoint;

public class GuestAcceptsInvitationEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<GuestAcceptsInvitationRequest>
    .WithoutResponse
{
    [HttpPost("/events/guest-accepts-invitation")]
    public override async Task<ActionResult> HandleAsync(GuestAcceptsInvitationRequest request)
    {
        var cmdResult = GuestAcceptsInvitationCommand.Create(request.EventId, request.GuestId);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record GuestAcceptsInvitationRequest(string EventId, string GuestId);