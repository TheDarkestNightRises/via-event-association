using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event;

public class EventAggregate : AggregateRoot<EventId>
{
    internal EventTitle EventTitle { get; set; }
    internal EventDescription EventDescription { get; set; }
    internal EventVisibility EventVisibility { get; set; }
    internal EventCapacity EventCapacity { get; set; }
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

    internal EventAggregate(EventId eventId) : base(eventId) {}

    public static Result<EventAggregate> Create()
    {
        var id = EventId.Create();
        var statusResult = EventStatus.Draft;
        var capacityResult = EventCapacity.Create(5);
        var titleResult = EventTitle.Create("Working Title");
        var descriptionResult = EventDescription.Create("");
        var visibilityResult = EventVisibility.Private;
        var aggregate = new EventAggregate(id, titleResult.PayLoad, descriptionResult.PayLoad,
                                          visibilityResult, capacityResult.PayLoad,
                                          statusResult);
        return aggregate; 
    }
    
    internal static EventAggregate Create(EventId eventId)
    {
        return new EventAggregate(eventId);
    }

    
    public Result<Void> UpdateEventDescription(EventDescription eventDescription)
    {
        if (EventStatus is EventStatus.Active)
        {
            return EventAggregateErrors.ActiveEventCantBeModified;
        }   
        
        if (EventStatus is EventStatus.Cancelled)
        {
            return EventAggregateErrors.CancelledEventCantBeModified;
        }

        var result = EventDescription.Validate(eventDescription);
        return result.Match<Result<Void>>(
            onPayLoad: _ =>
            {
                EventDescription = eventDescription;
                EventStatus = EventStatus.Draft;
                return new Void();
            },
            onError: errors => errors
        );
    }
    
    public Result<Void> UpdateEventTitle(string newUpdatedTitle)
    {
        // error if active status  with status error message explaing active status case 5
        if (EventStatus is EventStatus.Active)
        {
            return EventAggregateErrors.CanNotUpdateTitleOnActiveEvent;
        }
        // error if cancelled status  with status error message explaining cancelled status case 6
        if (EventStatus is EventStatus.Cancelled)
        {
            return EventAggregateErrors.CanNotUpdateTitleCancelledEvent;
        }
        var result = EventTitle.Create(newUpdatedTitle);
        return result.Match<Result<Void>>(
            onPayLoad: payLoad =>
            {
                EventTitle = payLoad;
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

    public Result<Void> SetNumberOfGuests(EventCapacity capacity)
    {
        switch (EventStatus)
        {
            case EventStatus.Active when (int)capacity < (int)EventCapacity:
                return EventAggregateErrors.NumberOfGuestsCanNotBeReduced;
            case EventStatus.Cancelled:
                return EventAggregateErrors.CancelledEventCantBeModified;
            case EventStatus.Draft:
            case EventStatus.Ready:
            default:
                EventCapacity = capacity;
                return new Void();
        }
    }
    
}
