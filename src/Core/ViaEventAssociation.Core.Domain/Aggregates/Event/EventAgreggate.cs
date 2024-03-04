using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event;

public class EventAggregate : AggregateRoot<EventId>
{
    internal EventTitle EventTitle { get; }
    internal EventDescription EventDescription { get; private set; }
    internal EventVisibility EventVisibility { get; set; }
    internal EventCapacity EventCapacity { get; }
    internal EventStatus EventStatus { get; set; }

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
    
    public Result<Void> UpdateEventDescription(string newDescription)
    {
        if (EventStatus is EventStatus.Active)
        {
            return EventAggregateErrors.ActiveEventCantBeModified;
        }   
        
        if (EventStatus is EventStatus.Cancelled)
        {
            return EventAggregateErrors.CancelledEventCantBeModified;
        }
        
        var result = EventDescription.Create(newDescription);
        
        return result.Match<Result<Void>>(
            onPayLoad: payLoad =>
            {
                EventDescription = payLoad;
                return new Void();
            },
            onError: errors => errors
        );
    }
    
    public Result<Void> MakeEventPrivate()
    {
        switch (EventStatus)
        {
            case EventStatus.Active:
                return EventAggregateErrors.CantMakeActiveEventPrivate;
            case EventStatus.Cancelled:
                return EventAggregateErrors.CantMakeCancelledEventPrivate;
            case EventStatus.Draft:
            case EventStatus.Ready:
            default:
                EventVisibility = EventVisibility.Private;
                return new Void();
        }
    }
    
    public Result<Void> MakeEventPublic()
    {
        if (EventStatus is EventStatus.Cancelled)
        {
            return EventAggregateErrors.CantMakeCancelledEventPublic;
        } 
        EventVisibility = EventVisibility.Public;
        return new Void();
    }
    
}
