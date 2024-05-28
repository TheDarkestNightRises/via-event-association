using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;

public class CreateNewEventEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint
    .WithoutRequest
    .WithoutResponse
{
    [HttpPost("/events")]
    public override async Task<ActionResult> HandleAsync()
    {
        var command = CreateNewEventCommand.Create().PayLoad;
        await dispatcher.DispatchAsync(command);
        // return ResultIS.isSUCCESS?
        //     Ok(command.Id)
        // : BadRequest()//Todo: All operations return void? is that usual???

        return Ok(command.Id);
    }
}