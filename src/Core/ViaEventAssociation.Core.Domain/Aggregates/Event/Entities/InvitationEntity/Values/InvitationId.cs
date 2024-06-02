using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;

public class InvitationId : ValueObject
{
    public Guid Id { get; }
    
    internal InvitationId()
    {
        Id = Guid.NewGuid();
    }

    internal InvitationId(Guid id)
    {
        Id = id;
    }

    public static InvitationId Create()
    {
        return new InvitationId();
    }
    
    public static Result<InvitationId> FromString(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return new InvitationId(guid);
        }
        return InvitationErrors.Invitation.InvalidId;
    }
    public static InvitationId FromGuid(Guid guid) => new InvitationId(guid);

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }
}