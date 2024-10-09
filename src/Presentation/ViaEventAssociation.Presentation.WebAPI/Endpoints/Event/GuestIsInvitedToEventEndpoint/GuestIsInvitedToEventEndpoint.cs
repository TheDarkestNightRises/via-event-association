using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.GuestIsInvitedToEventEndpoint;

public class GuestIsInvitedToEventEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<GuestIsInvitedToEventRequest>
    .WithoutResponse
{
    [HttpPost("events/invite-guest")]
    public override async Task<ActionResult> HandleAsync([FromBody] GuestIsInvitedToEventRequest request)
    {
        var cmdResult = GuestIsInvitedToEventCommand.Create(request.Id, request.GuestId);
        if (cmdResult.IsFailure)
        {
            return BadRequest(cmdResult.Errors);
        }

        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record GuestIsInvitedToEventRequest(string Id, string GuestId);
