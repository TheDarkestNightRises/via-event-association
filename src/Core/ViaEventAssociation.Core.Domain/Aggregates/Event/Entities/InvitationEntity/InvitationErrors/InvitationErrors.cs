using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;

public static class InvitationErrors
{
    public static class Invitation
    {
        private const string Code = "EventAggregate.Invitation";
        
            public static Error CantInviteGuestToDraftOrCancelledEvent => new(Code, "Guests can only be invited when the event is ready or active.");
            public static Error CantInviteGuestIfEventIsFull => new(Code, "Guests can't be invited if the event is full.");
        }
    }
