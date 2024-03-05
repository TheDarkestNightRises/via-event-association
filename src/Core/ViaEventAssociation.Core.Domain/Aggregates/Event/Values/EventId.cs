using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventId : ValueObject
{
    internal Guid Id { get; }

    private EventId()
    {
        Id = Guid.NewGuid();
    }

    public static Result<EventId> Create()
    {
        return new EventId();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }
}
