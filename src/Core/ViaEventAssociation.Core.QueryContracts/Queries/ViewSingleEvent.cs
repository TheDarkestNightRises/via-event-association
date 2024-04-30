using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public abstract class ViewSingleEvent
{
    public record Query(string Id) : IQuery<Answer>;

    public record Answer(ViewSingleEventInfo SingleEvent);

    public record ViewSingleEventInfo(
        string Title,
        string Description,
        string Start,
        string End,
        string Visibility,
        int NumberOfGuests
    );
}