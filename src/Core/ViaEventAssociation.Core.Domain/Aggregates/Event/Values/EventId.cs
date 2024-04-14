using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventId : ValueObject
{
    public Guid Id { get; }

    internal EventId()
    {
        Id = Guid.NewGuid();
    }

    internal EventId(Guid id)
    {
        Id = id;
    }

    public static EventId Create()
    {
        return new EventId();
    }
    
    public static Result<EventId> FromString(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return new EventId(guid);
        }

        return EventAggregateErrors.InvalidId;
    }
    
    public static EventId FromGuid(Guid guid) => new EventId(guid);
    
    
    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }
}
