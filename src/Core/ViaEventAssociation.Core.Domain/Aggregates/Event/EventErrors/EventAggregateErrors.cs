﻿using ViaEventAssociation.Core.Tools.OperationResult;
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
}