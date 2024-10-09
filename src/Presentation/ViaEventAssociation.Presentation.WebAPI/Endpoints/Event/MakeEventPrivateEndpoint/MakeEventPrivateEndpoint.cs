using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.MakeEventPrivateEndpoint;

public class MakeEventPrivateEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<MakeEventPrivateRequest>
    .WithoutResponse
{
    [HttpPost("events/private-event")]
    public override async Task<ActionResult> HandleAsync([FromBody] MakeEventPrivateRequest request)
    {
        var cmdResult = MakeEventPrivateCommand.Create(request.EventId);
        if (cmdResult.IsFailure)
            return BadRequest(cmdResult.Errors);
        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record MakeEventPrivateRequest(string EventId);