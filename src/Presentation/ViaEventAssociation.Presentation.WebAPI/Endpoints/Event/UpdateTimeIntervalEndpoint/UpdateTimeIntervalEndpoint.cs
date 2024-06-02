using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateTimeIntervalEndpoint;

public class UpdateTimeIntervalEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<UpdateTimeIntervalRequest>
    .WithoutResponse
{
    [HttpPost("/events/update-time-interval")]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] UpdateTimeIntervalRequest request)
    {
        var cmdResult = UpdateTimeIntervalCommand.Create(request.EventId, request.Start, request.End);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record UpdateTimeIntervalRequest(string EventId, DateTime Start, DateTime End);