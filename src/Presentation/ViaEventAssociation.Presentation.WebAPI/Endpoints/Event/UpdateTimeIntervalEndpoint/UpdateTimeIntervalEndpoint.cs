using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateTimeIntervalEndpoint;

public class UpdateTimeIntervalEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<UpdateTimeIntervalRequest>
    .WithoutResponse
{
    [HttpPost("/event/update-time-interval")]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] UpdateTimeIntervalRequest request)
    {
        try
        {
            var cmdResult = UpdateTimeIntervalCommand.Create(request.EventId, request.Start, request.End);
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
public record UpdateTimeIntervalRequest(string EventId, DateTime Start, DateTime End);