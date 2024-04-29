using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Event.CreateNewEventEndpoint;


public class CreateNewEventEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint 
    .WithRequest<CreateNewEventEndpoint.Request>
    .WithResponse<CreateNewEventEndpoint.Response>
{
    [HttpPost ("/events/create")]
    public override Task<ActionResult<Response>> HandleAsync(Request request)
    {
        throw new NotImplementedException();
    }
    public new record Request();
    public new record Response();
}

