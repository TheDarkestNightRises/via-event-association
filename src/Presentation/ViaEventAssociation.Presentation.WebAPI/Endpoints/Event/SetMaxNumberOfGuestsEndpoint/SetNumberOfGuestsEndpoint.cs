using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.SetMaxNumberOfGuestsEndpoint;

public class SetNumberOfGuestsEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<SetMaxNumberOfGuestsRequest>
    .WithoutResponse
{
    [HttpPost("/event/set-capacity")]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] SetMaxNumberOfGuestsRequest request)
    {
        try
        {
            var cmdResult = SetMaxNumberOfGuestsCommand.Create(request.EventId, request.Capacity);
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
public record SetMaxNumberOfGuestsRequest(string EventId, int Capacity);