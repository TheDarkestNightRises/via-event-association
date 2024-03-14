using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;

public static class EventAggregateErrors
{
    public static readonly Error CantMakeCancelledEventPublic = new Error("EventStatus.CannotMakeCancelledEventPublic",
        "Cannot make a cancelled event public.");
    
    public static readonly Error CantMakeCancelledEventPrivate = new Error("EventStatus.CannotMakeCancelledEventPrivate",
        "Cannot make a cancelled event private.");
    
    public static readonly Error CantMakeActiveEventPrivate = new Error("EventStatus.CannotMakeActiveEventPublic",
        "Cannot make an active event private.");
    
    public static readonly Error EventDescriptionIncorrectLength = new Error("EventDescription.EventDescriptionIncorrectLength",
        "The description length should be between 0 and 250 (inclusive).");
    
    public static readonly Error EventDescriptionCantBeNull = new Error("EventDescription.EventDescriptionIncorrectLength",
        "The description cannot be null.");
    
    public static readonly Error CancelledEventCantBeModified = new Error("Event.CancelledEventCantBeModified",
        "Cancelled event can't be modified");
    
    public static readonly Error ActiveEventCantBeModified = new Error("Event.CancelledEventCantBeModified",
        "Active event can't be modified");
    
    public static readonly Error CanNotUpdateTitleOnActiveEvent = new Error("Event.CanNotUpdateTitleOnActiveEvent",
        "Event could not be updated while active");
    
    public static readonly Error CanNotUpdateTitleCancelledEvent = new Error("Event.CanNotUpdateTitleCancelledEvent",
        "Event could not be updated while cancelled");
    
    public static readonly Error TitleCanNotBeUpdatedWithNullValue = new Error("Event.TitleCanNotBeUpdatedWithNullValue",
        "Title input can not be null");
    
    public static readonly Error TitleUpdateInputNotValid = new Error("Event.TitleUpdateInputNotValid",
        "Update title needs to be between 3 and 75");

    public static readonly Error EventCapacityCantBeNull = new Error("Event.EventCapacityCantBeNull",
        "Number of guests needs to be between 5 and 50");
    
    public static readonly Error EventCapacityCannotBeNegative = new Error("Event.EventCapacityCannotBeNegative",
        "Number of guests needs to be at least 5");
    
    public static readonly Error EventCapacityExceeded = new Error("Event.EventCapacityExceeded",
        "Number of guests needs to be at most 50");

    public static readonly Error NumberOfGuestsCanNotBeReduced = new Error("Event.NumberOfGuestsCanNotBeReduced",
        "Maximum number of guests of an active cannot be reduced");
    
    public static readonly Error CancelledEventCanNotBeReadied = new Error("Event.CancelledEventCanNotBeReadied",
        "Cancelled event can not be readied");

    public static readonly Error CanNotReadyAnEventWithDefaultTitle = new Error("Event.CanNotReadyAnEventWithDefaultTitle",
        "Can not ready an event with default title");

    public static readonly Error CanNotReadyAnEventWithNoTitle = new Error("Event.CanNotReadyAnEventWithNoTitle",
        "Can not ready an event with no title");

    public static readonly Error CanNotReadyAnEventWithNoDescription = new Error("Event.CanNotReadyAnEventWithNoDescription",
        "Can not ready an event with no description");

    public static readonly Error CanNotReadyAnEventWithNoVisibility = new Error("Event.CanNotReadyAnEventWithNoVisibility",
        "Can no ready an event with no visibility");
    
    public static readonly Error CanNotReadyAnEventWithNoTimeInterval = new Error("Event.CanNotReadyAnEventWithNoTimeInterval",
        "Can no ready an event with no time interval");
    
    public static readonly Error CanNotReadyAnEventWithTimeIntervalSetInThePast = new Error("Event.CanNotReadyAnEventWithTimeIntervalSetInThePast",
        "Can no ready an event with time interval set in the past");
    
    public static readonly Error CantParticipateInPrivateEvent = new Error("Event.CantParticipateInPrivateEvent",
        "Can not participate in a private event");
    
    public static readonly Error CantParticipateIfEventIsNotActive = new Error("Event.CantParticipateIfEventIsNotActive",
        "Can not participate because event is not active");
    
    public static readonly Error GuestAlreadyRegistered = new Error("Event.GuestAlreadyRegistered",
        "Can not participate again, because you are already registered in the event");
    // Errors
    public static readonly Error CancelledEventCantBeActivated = new Error("Event.CancelledEventCantBeActivated",
        "Cancelled event can't be activated");
    
    public static readonly Error InvalidEventData = new Error("Event.InvalidEventData",
        " Input can not be Invalid Data");
    public static readonly Error EndTimeBeforeStartTime = new Error("Event.EndTimeBeforeStartTime",
        "End time of event cannot be before Start time");
    public static readonly Error EventDurationOufOfRange = new Error("Event.EventDurationOutOfRange",
        "Event needs to be between 1 and 10 hours long");
    public static readonly Error EventInThePast = new Error("Event.EventInThePast",
        "Event cannot be in the past");
    public static readonly Error TimeIntervalUnavailable = new Error("Event.EventIntervalUnavailable",
        "Event cannot be held between 01:00 and 08:00 and can0not start after midnight");
    public static readonly Error CanNotChangeTimeOfActiveEvent = new Error("Event.CannotChangeTimeOfActiveEvent",
        "Cannot change the time of an active event");
    public static readonly Error CanNotChangeTimeOfCancelledEvent = new Error("Event.CanNotChangeTimeOfCancelledEvent",
        "Cannot change the time of a cancelled event");
    public static readonly Error CanNotParticipateInPastEvent = new Error("Event.CanNotParticipateInPastEvent",
        "Cannot participate in an event which has already passed");
    public static readonly Error CanNotParticipateInUndatedEvent = new Error("Event.CanNotParticipateInUndatedEvent",
        "Cannot participate in undated events");
}