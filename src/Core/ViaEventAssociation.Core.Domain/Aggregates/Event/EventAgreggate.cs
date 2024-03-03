using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event;

public class EventAggregate : AggregateRoot<EventId>
{
    internal EventTitle EventTitle { get; }
    internal EventDescription EventDescription { get; }
    internal EventVisibility EventVisibility { get; }
    internal EventCapacity EventCapacity { get; }
    internal EventStatus EventStatus { get; }

    private EventAggregate(EventId id, EventTitle title, EventDescription description,
                           EventVisibility visibility, EventCapacity capacity,
                           EventStatus status) : base(id)
    {
        EventTitle = title;
        EventDescription = description;
        EventVisibility = visibility;
        EventCapacity = capacity;
        EventStatus = status;
    }

    public static Result<EventAggregate> Create()
    {
        // var titleResult = EventTitle.Create(title);
        // var descriptionResult = EventDescription.Create(description);
        // var visibilityResult = EventVisibility.Create(visibility);
        // var capacityResult = EventCapacity.Create(capacity);
        // var statusResult = EventStatus.Create();   

        
        // var aggregate = new EventAggregate(id, titleResult.Value, descriptionResult.Value,
        //                                   visibilityResult.Value, capacityResult.Value,
        //                                   statusResult.Value);

        // return aggregate; 
        throw new NotImplementedException();
    }
}
