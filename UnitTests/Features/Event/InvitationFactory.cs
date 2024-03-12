using ViaEventAssociation.Core.Domain.Aggregates.Entity;
using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Event;

public class InvitationFactory
{
    private Invitation _invitation;

    private InvitationFactory()
    {
        var id = InvitationId.Create();
        _invitation = Invitation.Create(id);
    }
    
    public static InvitationFactory Init()
    {
        return new InvitationFactory();
    }
    
    public Invitation Build()
    {
        return _invitation;
    }
    
    public InvitationFactory WithStatus(InvitationStatus status)
    {
        _invitation.InvitationStatus = status;
        return this;
    }    
    
    public InvitationFactory WithGuest(GuestId guestId)
    {
        _invitation.GuestId = guestId;
        return this;
    }
}