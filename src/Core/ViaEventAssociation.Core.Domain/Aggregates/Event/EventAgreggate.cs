using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
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
    internal List<GuestId> EventGuests { get; set; } = [];


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

    internal EventAggregate(EventId eventId) : base(eventId)
    {
    }

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

    public Result<Void> UpdateEventTitle(EventTitle eventTitle)
    {
        // Error if active status with status error message explaining the active status case
        if (EventStatus is EventStatus.Active)
        {
            return EventAggregateErrors.CanNotUpdateTitleOnActiveEvent;
        }

        // Error if cancelled status with status error message explaining the cancelled status case
        if (EventStatus is EventStatus.Cancelled)
        {
            return EventAggregateErrors.CanNotUpdateTitleCancelledEvent;
        }

        // Assuming Validate method for EventTitle is available in EventTitle class
        EventTitle = eventTitle;
        EventStatus = EventStatus.Draft;
        return new Void();
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
                var result = EventCapacity.Validate(capacity);
                return result.Match<Result<Void>>(
                    onPayLoad: _ =>
                    {
                        EventCapacity = capacity;
                        return new Void();
                    },
                    onError: errors => errors
                );
        }
    }

    public Result<Void> MakeEventReady()
    {
        if (EventStatus is EventStatus.Cancelled)
        {
            return EventAggregateErrors.CancelledEventCanNotBeReadied;
        }

        if (EventStatus is not EventStatus.Draft) return new Void();
        var defaultTitle = Create().PayLoad.EventTitle;
        var noTitle = new EventTitle("");
        var noDescription =  Create().PayLoad.EventDescription;
        var minCapacity = EventCapacity.Create(5).PayLoad;
        var maxCapacity = EventCapacity.Create(50).PayLoad;
        if (EventTitle == defaultTitle)
        {
            return EventAggregateErrors.CanNotReadyAnEventWithDefaultTitle;
        }
        if (EventTitle == noTitle)
        {
            return EventAggregateErrors.CanNotReadyAnEventWithNoTitle;
        }
        if (EventDescription == noDescription)
        {
            return EventAggregateErrors.CanNotReadyAnEventWithNoDescription;
        }
        // Todo: add time check
        if (EventVisibility is EventVisibility.None)
        {
            return EventAggregateErrors.CanNotReadyAnEventWithNoVisibility;
        }
        if ((int)EventCapacity < (int)minCapacity)
        {
            return EventAggregateErrors.EventCapacityCannotBeNegative;
        }
        if ((int)EventCapacity > (int)maxCapacity)
        {
            return EventAggregateErrors.EventCapacityExceeded;
        }
        EventStatus = EventStatus.Ready;
        return new Void();
    }
    
    // Make an event active
    public Result<Void> MakeEventActive()
    {
        if (EventStatus == EventStatus.Draft)
        {
            Result<Void> makeEventReady = MakeEventReady();
                makeEventReady.Match<Result<Void>>(
                onPayLoad: _ =>
                {
                    EventStatus = EventStatus.Active;
                    return new Void();
                },
                onError: errors => errors
            );
                
            if (EventStatus == EventStatus.Ready)
            {
                EventStatus = EventStatus.Active;
                return new Void();
            }
        }
        if (EventStatus == EventStatus.Active)
        {
            EventStatus = EventStatus.Active;
            return new Void();
        }
        return new Void();
    }
    
    public Result<Void> ParticipateInPublicEvent(GuestId guestId)
    {
        if (EventVisibility is EventVisibility.Private)
        {
            return EventAggregateErrors.CantParticipateInPrivateEvent;
        }
        if (EventStatus is not EventStatus.Active)
        {
            return EventAggregateErrors.CantParticipateIfEventIsNotActive;
        }
        if (EventGuests.Count >= (int)EventCapacity)
        {
            return EventAggregateErrors.EventCapacityExceeded;
        }
        if (EventGuests.Contains(guestId))
        {
            return EventAggregateErrors.GuestAlreadyRegistered;
        }
        EventGuests.Add(guestId);
        return new Void();
    }
    
    public Result<Void> CancelParticipationInEvent(GuestId guestId)
    {
        if (!EventGuests.Contains(guestId))
        {
            return new Void();
        }
        EventGuests.Remove(guestId);
        return new Void();
    }
}


