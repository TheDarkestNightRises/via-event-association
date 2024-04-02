using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest;

public class GuestAggregate : AggregateRoot<GuestId>
{
    internal GuestViaEmail GuestViaEmail { get; set; }
    internal GuestFirstName GuestFirstName { get; set; }
    internal GuestLastName GuestLastName { get; set; }
    internal GuestPictureUrl GuestPictureUrl { get; set; }

    internal GuestAggregate(GuestId id) : base(id)
    {
    }
    
    private GuestAggregate(GuestId id, GuestViaEmail guestViaEmail, GuestFirstName guestFirstName, GuestLastName guestLastName, GuestPictureUrl guestPictureUrl) : base(id)
    {
        GuestViaEmail = guestViaEmail;
        GuestFirstName = guestFirstName;
        GuestLastName = guestLastName;
        GuestPictureUrl = guestPictureUrl;
    }
    
    internal static GuestAggregate Create(GuestId id)
    {
        return new GuestAggregate(id);
    }

    public GuestAggregate Create(GuestViaEmail email, GuestFirstName firstName, GuestLastName lastName, GuestPictureUrl pictureUrl)
    {
        var id = GuestId.Create();
        return new GuestAggregate(id, email, firstName, lastName, pictureUrl);
    }
}