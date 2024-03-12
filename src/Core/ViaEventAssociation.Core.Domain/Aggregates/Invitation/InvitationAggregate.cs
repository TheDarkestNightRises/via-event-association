using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Entity;

public class InvitationAggregate : Entity<InvitationId>
{
    internal InvitationId InvitationId { get; set; }
    internal GuestId GuestId { get; set; }
    internal bool Approved { get; set; }
    internal InvitationStatus Status { get; set; }



    internal InvitationAggregate(InvitationId id, GuestId guestId, bool approved) :
        base(id)
    {
        InvitationId = id;
        GuestId = guestId;
        Approved = approved;
    }

    internal InvitationAggregate(InvitationId invitationId, InvitationStatus status) : base(invitationId)
    {
        Status = status;
    }

    public static Result<InvitationAggregate>Create( GuestId guestId, bool approved )
    {
        var id = InvitationId.Create();
        var statusResult = InvitationStatus.Pending;
        var aggregate = new InvitationAggregate(id, guestId, approved);
        return aggregate;
    }
  
    private Result<Void> InviteGuestToEvent(EventAggregate eventAggregate)
    {
        if (eventAggregate.EventStatus == EventStatus.Ready || eventAggregate.EventStatus == EventStatus.Active)
        {
            Approved = true;
            eventAggregate.EventParticipants.Add(GuestId);
            return new Void();
        }
        {
            return new Void();
        }
    }
}
