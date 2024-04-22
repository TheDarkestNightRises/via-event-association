using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class UnpublishedEvents
{
    public record Query() : IQuery<Answer>;

    public record Answer(
        List<EventDetails> CanceledEvents,
        List<EventDetails> DraftEvents,
        List<EventDetails> ReadyEvents);

    public record EventDetails(string Title);
}