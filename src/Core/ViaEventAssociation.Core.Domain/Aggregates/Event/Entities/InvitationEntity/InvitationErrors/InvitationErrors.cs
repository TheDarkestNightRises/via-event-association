using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;

public static class InvitationErrors
{
    public static class Invitation
    {
        private const string Code = "EventAggregate.Invitation";
        
            public static Error CantInviteGuestToDraftOrCancelledEvent => new(Code, "Guests can only be invited when the event is ready or active.");
            public static Error CantInviteGuestIfEventIsFull => new(Code, "Guests can't be invited if the event is full.");
            public static Error InvalidId => new(Code, "Invitation id is not valid.");
            public static Error EventMustBeActiveToAcceptInvitation => new(Code, "Event must be active to accept invitation.");
            public static Error InvitationMustBeInPendingToAccept => new(Code, "Invitation must be in pending to accept.");
            public static Error EventShouldNotBeFullToAcceptInvitation => new(Code, "Event should not be full to accept invitation.");
            public static Error GuestHasNotBeenInvitedToTheEvent => new(Code, "Guest has not been invited to the event.");
    }
    }
