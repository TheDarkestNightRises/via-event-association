using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Guest;

public class GuestFactory
{
    private GuestAggregate _guestAggregate;

    private GuestFactory()
    {
        var id = GuestId.Create();
        _guestAggregate = GuestAggregate.Create(id);
    }
    
    public static GuestFactory Init()
    {
        return new GuestFactory();
    }
    
    public GuestAggregate Build()
    {
        return _guestAggregate;
    }
    
    public GuestFactory WithFirstName(GuestFirstName firstName)
    {
        _guestAggregate.GuestFirstName = firstName;
        return this;
    }
    
    public GuestFactory WithLastName(GuestLastName lastName)
    {
        _guestAggregate.GuestLastName = lastName;
        return this;
    }

    public GuestFactory WithEmail(GuestViaEmail email)
    {
        _guestAggregate.GuestViaEmail = email;
        return this;
    }
    
    public GuestFactory WithPictureUrl(GuestPictureUrl guestPictureUrl)
    {
        _guestAggregate.GuestPictureUrl = guestPictureUrl;
        return this;
    }
    
    public static GuestAggregate ValidGuest()
    {
        return Init()
            .WithFirstName(new GuestFirstName("John"))
            .WithLastName(new GuestLastName("Doe"))
            .WithPictureUrl(new GuestPictureUrl("https://via.placeholder.com/150"))
            .WithEmail(new GuestViaEmail("sampleemail@gmail.com"))
            .Build();
    }

}