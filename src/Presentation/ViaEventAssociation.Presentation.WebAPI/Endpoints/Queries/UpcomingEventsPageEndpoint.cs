using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class UpcomingEventsEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<UpcomingEventsPageRequest>
    .WithResponse<UpcomingEventsResponse>
{
    [HttpGet("/upcoming-events/")]
    public override async Task<ActionResult<UpcomingEventsResponse>> HandleAsync([FromQuery] UpcomingEventsPageRequest request)
    {
        var query = mapper.Map<UpcomingEventPage.Query>(request);
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<UpcomingEventsResponse>(answer);
        return Ok(response);
    }
}

public record UpcomingEventsPageRequest(
    [FromQuery] int PageSize,
    [FromQuery] int PageNumber,
    [FromQuery] string? EventName);

public record UpcomingEventsResponse(List<EventOverview> EventOverviews);

public record EventOverview(
    string Title,
    string Description,
    string Start,
    string End,
    string Visibility,
    int MaxCapacity,
    int NumberOfParticipants);