namespace ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;

public class InvitationId : ValueObject
{
    internal Guid Id { get; }
    
    private InvitationId()
    {
        Id = Guid.NewGuid();
    }
    
    public static InvitationId Create()
    {
        return new InvitationId();
    }
    
    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }
}