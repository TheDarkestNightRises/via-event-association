using ViaEventAssociation.Core.Domain.Aggregates.Entity;
using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;

namespace UnitTests.Features.Invitation;

public class InvitationFactory
{
    private InvitationAggregate _invitationAggregate;

    private InvitationFactory()
    {
        var id = InvitationId.Create();
        _invitationAggregate =  InvitationAggregate.Create(id, false); 
    }

    public static InvitationFactory Init()
    {
        return new InvitationFactory();
    }
    
    public InvitationAggregate Build()
    {
        return _invitationAggregate;
    }
    
    public InvitationFactory WithStatus(InvitationStatus status)
    {
        _invitationAggregate.Status = status;
        return this;
    }

    public InvitationFactory WithApproval(bool approved)
    {
        _invitationAggregate.Approved = approved;
        return this;
    }
}