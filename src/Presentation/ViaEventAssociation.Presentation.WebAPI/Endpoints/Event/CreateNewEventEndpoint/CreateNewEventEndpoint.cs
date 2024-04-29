using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;

public class CreateNewEventEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<CreateNewEventEndpoint.Request>
    .WithResponse<CreateNewEventEndpoint.Response>
{
    [HttpPost("/events/create")]
    public override async Task<ActionResult<Response>> HandleAsync([FromBody] Request request)
    {
        var command = mapper.Map<Request, CreateNewEventCommand>(request);
        await dispatcher.DispatchAsync(command);
        return Ok(); //Todo: All operations return void? is that usual???
    }

    public new record Request(Guid EventId); // Is there even a request for this one

    public new record Response();
}