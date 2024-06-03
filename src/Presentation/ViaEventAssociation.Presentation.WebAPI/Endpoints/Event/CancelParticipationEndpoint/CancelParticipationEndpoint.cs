using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CancelParticipationEndpoint;

public class CancelParticipationEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<CancelParticipationRequest>
    .WithoutResponse
{
    [HttpPost("events/cancel-participation")]
    public override async Task<ActionResult> HandleAsync([FromBody] CancelParticipationRequest request)
    {
        var cmdResult = CancelParticipationInEventCommand.Create(request.EventId, request.GuestId);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record CancelParticipationRequest(string EventId, string GuestId);