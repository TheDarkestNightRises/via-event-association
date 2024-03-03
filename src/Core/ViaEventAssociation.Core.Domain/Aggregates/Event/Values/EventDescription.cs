
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventDescription : ValueObject
{
    protected string Description { get; }

    private EventDescription(string decription)
    {
        Description = decription;
    }

    public static Result<EventDescription> Create(string description)
    {
        return new EventDescription(description);
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
