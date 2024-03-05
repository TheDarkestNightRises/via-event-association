namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestId : ValueObject
{
    internal Guid Id { get; }

    private GuestId()
    {
        Id = Guid.NewGuid();
    }

    public static GuestId Create()
    {
        return new GuestId();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }

}