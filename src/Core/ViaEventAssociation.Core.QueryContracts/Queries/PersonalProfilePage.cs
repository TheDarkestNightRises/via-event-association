using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class PersonalProfilePage
{
    public record Query(string GuestId, TimeProvider? TimeProvider = null) : IQuery<Answer>;

    public record Answer(PersonalProfile PersonalProfile);

    public record PersonalProfile(
        string FullName,
        string Email,
        string ProfilePicUrl,
        int UpcomingEventCount,
        List<UpcomingEvents> UpcomingEvents,
        List<PastEvents> PastEvents,
        int PendingInvitationsCount);
    
    public record UpcomingEvents(string Title,int NumberOfParticipants, string Start);
    public record PastEvents(string Title);
}