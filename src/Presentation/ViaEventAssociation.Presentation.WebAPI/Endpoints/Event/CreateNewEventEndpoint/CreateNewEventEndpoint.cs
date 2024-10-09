using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventAssociation.Presentation.WebAPI.Filters;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;

public class CreateNewEventEndpoint(ICommandDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithoutRequest
    .WithResponse<CreateNewEventResponse>
{
    [HttpPost("events/create")]
    public override async Task<ActionResult<CreateNewEventResponse>> HandleAsync()
    {
        var command = CreateNewEventCommand.Create().PayLoad;
        var result = await dispatcher.DispatchAsync(command);
        return result.ToResponse(_ => new CreateNewEventResponse(command.Id.Id));
    }
}

public record CreateNewEventResponse(Guid Id);