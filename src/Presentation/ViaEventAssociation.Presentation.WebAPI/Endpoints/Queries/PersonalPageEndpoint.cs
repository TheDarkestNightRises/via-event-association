using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class PersonalPageEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint
    .WithRequest<PersonalPageRequest>
    .WithResponse<PersonalPageResponse>
{
    [HttpGet("personalPage/{GuestId}")]
    public override async Task<ActionResult<PersonalPageResponse>> HandleAsync(
        [FromRoute] PersonalPageRequest request)
    {
        var query = mapper.Map<PersonalProfilePage.Query>(request);
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<PersonalPageResponse>(answer);
        return Ok(response);
    }
}

public record PersonalPageRequest([FromRoute] string GuestId);

public record PersonalPageResponse(
    string FullName,
    string Email,
    string ProfilePicUrl,
    int UpcomingEventCount,
    List<UpcomingEvents> UpcomingEvents,
    List<PastEvents> PastEvents,
    int PendingInvitationsCount);

public record UpcomingEvents(string Title,int NumberOfParticipants, string Start);
public record PastEvents(string Title);

