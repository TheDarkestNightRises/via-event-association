
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventCapacity : ValueObject
{
    protected int Capacity { get; }

    internal EventCapacity(int capacity)
    {
        Capacity = capacity;
    }

       public static Result<EventCapacity> Create(int capacity)
    {
        var validationResult = Validate(capacity);
        return validationResult.Match<Result<EventCapacity>>(
            onPayLoad: _ => new EventCapacity(capacity!),
            onError: errors => errors); 
    }
       
    private static Result<Void> Validate(int? capacity)
    {
        return capacity switch
        {
            null => EventAggregateErrors.EventCapacityCantBeNull,
            < 5 => EventAggregateErrors.EventCapacityCannotBeNegative,
            > 50 => EventAggregateErrors.EventCapacityExceeded,
            _ => new Void()
        };
    }
    
    public static Result<Void> Validate(EventCapacity? capacity)
    {
        if (capacity == null)
        {
            return EventAggregateErrors.EventCapacityCantBeNull;
        }
        return (int)capacity switch
            {
                < 5 => EventAggregateErrors.EventCapacityCannotBeNegative,
                > 50 => EventAggregateErrors.EventCapacityExceeded,
                _ => new Void()
            };
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

