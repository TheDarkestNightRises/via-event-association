using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class UnpublishedEventsEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithoutRequest
    .WithResponse<UnpublishedEventsResponse>
{
    [HttpGet("/unpublished-events")]
    public override async Task<ActionResult<UnpublishedEventsResponse>> HandleAsync()
    {
        var query = new UnpublishedEvents.Query();
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<UnpublishedEventsResponse>(answer);
        return Ok(response);
    }
}

public record UnpublishedEventsResponse(
    List<EventDetails> CanceledEvents,
    List<EventDetails> DraftEvents,
    List<EventDetails> ReadyEvents);
    
public record EventDetails(string Title);
