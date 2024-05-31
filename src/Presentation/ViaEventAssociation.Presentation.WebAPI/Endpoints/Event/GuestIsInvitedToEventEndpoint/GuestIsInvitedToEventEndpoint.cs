using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.GuestIsInvitedToEventEndpoint;

public class GuestIsInvitedToEventEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<GuestIsInvitedToEventRequest>
    .WithoutResponse
{
    [HttpPost("events/{Id}/invite-guest")]
    public override async Task<ActionResult> HandleAsync([FromRoute] GuestIsInvitedToEventRequest request)
    {
        var cmdResult = GuestIsInvitedToEventCommand.Create(request.Id, request.RequestBody.GuestId);
        if (cmdResult.IsFailure)
        {
            return BadRequest(cmdResult.Errors);
        }

        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
}

public class GuestIsInvitedToEventRequest
{
    [FromRoute] public string Id { get; set; }

    [FromBody] public Body RequestBody { get; set; }

    public record Body(string GuestId);
}