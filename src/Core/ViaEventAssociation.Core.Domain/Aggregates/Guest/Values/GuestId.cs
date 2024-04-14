using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestId : ValueObject
{
    public Guid Id { get; }

    private GuestId()
    {
        Id = Guid.NewGuid();
    }
    private GuestId(Guid guid)
    {
        Id = guid;
    }
    public static GuestId Create()
    {
        return new GuestId();
    }
    public static Result<GuestId> FromString(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return new GuestId(guid);
        }
        return GuestAggregateErrors.Id.InvalidId;
    }
    public static GuestId FromGuid(Guid guid) => new GuestId(guid);
    
    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Id;
    }
}