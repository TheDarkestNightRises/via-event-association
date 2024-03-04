using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;

public static class EventAggregateErrors
{
        public static readonly Error CantMakeCancelledEventPublic = new Error("EventPublic.CannotMakeCancelledEventPublic",
            "Cannot make a cancelled event public.");
}