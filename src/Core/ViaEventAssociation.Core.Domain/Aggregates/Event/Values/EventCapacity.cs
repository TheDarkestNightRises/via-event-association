
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventCapacity : ValueObject
{
    protected int Capacity { get; }

    private EventCapacity(int capacity)
    {
        Capacity = capacity;
    }

       public static Result<EventCapacity> Create(int capacity)
    {
        return new EventCapacity(capacity);
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Capacity;
    }

    public static explicit operator int(EventCapacity eventCapacity)
    {
        return eventCapacity.Capacity;
    }

}

