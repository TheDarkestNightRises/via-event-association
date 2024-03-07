using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Event.CancelParticipationInEvent;

public class CancelParticipationInEventUnitTests
{
    // UC12.S1
    [Fact]
    public void GivenEvent_AndRegisteredGuest_WhenCancelParticipation_ThenEventRemovesTheGuest_AndTheNumberOfGuestDecreases()
    {
        var guestAggregate = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        var initialNumberOfGuests = eventAggregate.EventGuests.Count;
        eventAggregate.CancelParticipationInEvent(guestAggregate.Id);
        Assert.Equal(initialNumberOfGuests - 1, eventAggregate.EventGuests.Count);
    }
    
    // UC12.S1
    [Fact]
    public void GivenEvent_AndRegisteredGuest_WhenCancelParticipation_ThenEventRemovesTheGuest_AndGuestCanNotBeFoundInEvent()
    {
        var guestAggregate = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        var initialNumberOfGuests = eventAggregate.EventGuests.Count;
        eventAggregate.CancelParticipationInEvent(guestAggregate.Id);
        Assert.DoesNotContain(guestAggregate.Id, eventAggregate.EventGuests);
    }
    
    // UC12.S2
    [Fact]
    public void GivenEvent_AndRegisteredGuest_WhenCancelParticipation_AndGuestItNotInEvent_ThenNothingChanges_AndCapacityStaysTheSame()
    {
        var guestAggregate1 = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var guestAggregate2 = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var initialNumberOfGuests = eventAggregate.EventGuests.Count;
        eventAggregate.ParticipateInPublicEvent(guestAggregate1.Id);
        eventAggregate.CancelParticipationInEvent(guestAggregate2.Id);
        Assert.Equal(initialNumberOfGuests + 1, eventAggregate.EventGuests.Count);
    }
    
    // UC12.S2
    [Fact]
    public void GivenEvent_AndRegisteredGuest_WhenCancelParticipation_AndGuestItNotInEvent_ThenNothingChanges_AndGuestCanNotBeFoundInEvent()
    {
        var guestAggregate1 = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var guestAggregate2 = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var initialNumberOfGuests = eventAggregate.EventGuests.Count;
        eventAggregate.ParticipateInPublicEvent(guestAggregate1.Id);
        eventAggregate.CancelParticipationInEvent(guestAggregate2.Id);
        Assert.DoesNotContain(guestAggregate2.Id, eventAggregate.EventGuests);
    }
    
    // Todo: UC12.F1 add when date time is implemented
}