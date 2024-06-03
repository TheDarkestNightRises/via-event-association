using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Core.Tools.OperationResult;
using System;
using System.Threading.Tasks;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateEventTitleEndpoint;

public class UpdateEventTitleEndpoint(IMapper mapper, ICommandDispatcher dispatcher) : ApiEndpoint
    .WithRequest<UpdateEventTitleRequest>
    .WithoutResponse
{
    [HttpPost("events/update-event-title")]
    public override async Task<ActionResult> HandleAsync([FromBody] UpdateEventTitleRequest request)
    {

        var cmdResult = UpdateEventTitleCommand.Create(request.Id, request.NewTitle);
        if (cmdResult.IsFailure)
        {
            return BadRequest(cmdResult.Errors);
        }

        var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
        return result.ToResponse();
    }
}

public record UpdateEventTitleRequest(string Id, string NewTitle);
