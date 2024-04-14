namespace ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;

public class InvitationId : ValueObject
{
    public Guid Id { get; }
    
    private InvitationId()
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
    
    public static InvitationId FromGuid(Guid guid) => new InvitationId(guid);

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }
}