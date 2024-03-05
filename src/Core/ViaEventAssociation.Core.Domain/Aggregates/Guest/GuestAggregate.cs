using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest;

public class GuestAggregate : AggregateRoot<GuestId>
{
    internal GuestViaEmail GuestViaEmail { get; set; }
    internal GuestFirstName GuestFirstName { get; set; }
    internal GuestLastName GuestLastName { get; set; }
    internal GuestPictureUrl GuestPictureUrl { get; set; }
    
    internal GuestAggregate(GuestId id) : base(id) {}
    internal static GuestAggregate Create(GuestId id)
    {
        return new GuestAggregate(id);
    }
    
}