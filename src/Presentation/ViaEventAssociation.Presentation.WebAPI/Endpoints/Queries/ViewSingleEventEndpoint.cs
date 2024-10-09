using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class ViewSingleEventEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<ViewSingleEventRequest>
    .WithResponse<ViewSingleEventResponse>
{
    [HttpGet("events/{Id}")]
    public override async Task<ActionResult<ViewSingleEventResponse>> HandleAsync(
        [FromRoute] ViewSingleEventRequest request)
    {
        var query = mapper.Map<ViewSingleEvent.Query>(request);
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<ViewSingleEventResponse>(answer);
        return Ok(response);
    }
}

public record ViewSingleEventRequest([FromRoute] string Id);

public record ViewSingleEventResponse(
    string Title,
    string Description,
    string Start,
    string End,
    string Visibility,
    int NumberOfGuests);