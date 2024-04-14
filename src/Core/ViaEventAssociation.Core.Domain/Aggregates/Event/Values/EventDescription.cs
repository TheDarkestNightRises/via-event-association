using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventDescription : ValueObject
{
    public string Description { get; }

    internal EventDescription(string description)
    {
        Description = description;
    }

    public static Result<EventDescription> Create(string? description)
    {
        var validationResult = Validate(description);
        
        return validationResult.Match<Result<EventDescription>>(
            onPayLoad: _ => new EventDescription(description!),
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
    
    public static Result<Void> Validate(EventDescription? eventDescription)
    {
        if (eventDescription is null)
        {
            return EventAggregateErrors.EventDescriptionCantBeNull;
        }
        
        if (eventDescription.Description.Length > 250)
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
