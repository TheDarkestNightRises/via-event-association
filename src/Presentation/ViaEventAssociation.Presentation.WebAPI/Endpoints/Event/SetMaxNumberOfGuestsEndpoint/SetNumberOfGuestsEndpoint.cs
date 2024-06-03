using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.SetMaxNumberOfGuestsEndpoint;

public class SetNumberOfGuestsEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<SetMaxNumberOfGuestsRequest>
    .WithoutResponse
{
    [HttpPost("events/set-capacity")]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] SetMaxNumberOfGuestsRequest request)
    {
        var cmdResult = SetMaxNumberOfGuestsCommand.Create(request.EventId, request.Capacity);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record SetMaxNumberOfGuestsRequest(string EventId, int Capacity);