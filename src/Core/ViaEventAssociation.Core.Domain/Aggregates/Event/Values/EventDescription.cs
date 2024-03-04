using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventDescription : ValueObject
{
    internal string Description { get; }

    private EventDescription(string description)
    {
        Description = description;
    }

    public static Result<EventDescription> Create(string? description)
    {
        var validationResult = Validate(description);
        
        return validationResult.Match<Result<EventDescription>>(
            onValue: _ => new EventDescription(description!),
            onError: errors => errors
        );
    }

    private static Result<Void> Validate(string? description)
    {
        if (description is null)
        {
            return EventAggregateErrors.EventDescriptionCantBeNull;
        }
        
        if (description.Length > 250)
        {
            return EventAggregateErrors.EventDescriptionIncorrectLength;
        } 
        
        return new Void();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Description;
    }

    public static explicit operator string(EventDescription eventDescription)
    {
        return eventDescription.Description;
    }
}
