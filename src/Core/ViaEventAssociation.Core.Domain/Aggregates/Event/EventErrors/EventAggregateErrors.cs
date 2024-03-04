using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;

public static class EventAggregateErrors
{
    public static readonly Error CantMakeCancelledEventPublic = new Error("EventPublic.CannotMakeCancelledEventPublic",
        "Cannot make a cancelled event public.");
    
    public static readonly Error EventDescriptionIncorrectLength = new Error("EventDescription.EventDescriptionIncorrectLength",
        "The description length should be between 0 and 250 (inclusive).");
    
    public static readonly Error EventDescriptionCantBeNull = new Error("EventDescription.EventDescriptionIncorrectLength",
        "The description cannot be null.");
    
    public static readonly Error CancelledEventCantBeModified = new Error("Event.CancelledEventCantBeModified",
        "Cancelled event can't be modified");
    
    public static readonly Error ActiveEventCantBeModified = new Error("Event.CancelledEventCantBeModified",
        "Active event can't be modified");
}