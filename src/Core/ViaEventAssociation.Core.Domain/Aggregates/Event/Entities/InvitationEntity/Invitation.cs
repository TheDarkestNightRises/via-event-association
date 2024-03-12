using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;

public class Invitation : Entity<InvitationId>
{
    internal InvitationId InvitationId { get; set; }
    internal GuestId GuestId { get; set; }
    internal InvitationStatus InvitationStatus { get; set; }


    internal Invitation(InvitationId id, GuestId guestId, InvitationStatus invitationStatus) :
        base(id)
    {
        InvitationId = id;
        GuestId = guestId;
        InvitationStatus = invitationStatus;
    }

    private Invitation(InvitationId invitationId) : base(invitationId){}
  

    internal static Invitation Create(InvitationId invitationId)
    {
        return new Invitation(invitationId);
    }


    public static Result<Invitation> Create(GuestId guestId)
    {
        var id = InvitationId.Create();
        var status = InvitationStatus.Pending;
        return new Invitation(id, guestId, status);
    }
}