using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries.ViewSingleEvent;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries.ViewSingleEventEndpoint;

public class ViewSingleEventEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<ViewSingleEventRequest>
    .WithResponse<ViewSingleEventResponse>
{
    [HttpPost("/events/{id}")]
    public override async Task<ActionResult<ViewSingleEventResponse>> HandleAsync([FromRoute] ViewSingleEventRequest request)
    {
        var query = mapper.Map<Core.QueryContracts.Queries.ViewSingleEvent.Query>(request);
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<ViewSingleEventResponse>(answer);
        return Ok(response);
    }
}