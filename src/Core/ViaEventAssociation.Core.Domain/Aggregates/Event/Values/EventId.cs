using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventId : ValueObject
{
    internal Guid Id { get; }

    private EventId()
    {
        Id = Guid.NewGuid();
    }

    private EventId(Guid id)
    {
        this.Id = id;
    }

    public static EventId Create()
    {
        return new EventId();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }

    public static Result<EventId> FromString(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return new EventId(guid);
        }

        return EventAggregateErrors.InvalidId;
    }
}
