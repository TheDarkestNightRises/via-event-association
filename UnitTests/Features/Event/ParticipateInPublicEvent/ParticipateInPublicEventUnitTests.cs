using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Event.ParticipateInPublicEvent;

public class ParticipateInPublicEventUnitTests
{
    // UC11.S1
    [Fact]
    public void GivenEvent_AndStatusIsActive_AndEventIsPublic_AndNGuestLessThanCapacity_AndEventDidNotStart_ThenRegisterGuest_AndCheckForNewNumberOfGuest()
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
        var initialNumberOfGuests = eventAggregate.EventParticipants.Count;
        eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(initialNumberOfGuests + 1, eventAggregate.EventParticipants.Count);
    }
    
    // UC11.S1
    [Fact]
    public void GivenEvent_AndStatusIsActive_AndEventIsPublic_AndNGuestLessThanCapacity_AndEventDidNotStart_ThenRegisterGuest_AndCheckForGuestIdInList()
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
        Assert.Contains(guestAggregate.Id, eventAggregate.EventParticipants);
    }
    
    // UC11.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_AndEventIsPublic_ThenFailureMessageIsProvided()
    {
        var guestAggregate = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(EventAggregateErrors.CantParticipateIfEventIsNotActive, result.Errors.First());
    }
    // UC11.F1
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_AndEventIsPublic_ThenFailureMessageIsProvided()
    {
        var guestAggregate = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(EventAggregateErrors.CantParticipateIfEventIsNotActive, result.Errors.First());
    }
    
    // UC11.F1
    [Fact]
    public void GivenEvent_AndStatusIsReady_AndEventIsPublic_ThenFailureMessageIsProvided()
    {
        var guestAggregate = GuestFactory.Init()
            .WithEmail(GuestViaEmail.Create("321312@outlook.com").PayLoad)
            .WithFirstName(GuestFirstName.Create("Polo").PayLoad)
            .WithLastName(GuestLastName.Create("Marco").PayLoad)
            .WithPictureUrl(GuestPictureUrl.Create("picture.png").PayLoad)
            .Build();
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(EventAggregateErrors.CantParticipateIfEventIsNotActive, result.Errors.First());
    }
    
    // UC11.F2
    [Fact]
    public void GivenEvent_AndStatusIsActive_AndEventIsPublic_AndCapacityIsFull_ThenFailureMessageIsProvided()
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
        for (var i = 0; i < 25; i++)
        {
            var guestAggregate1 = GuestFactory.Init().Build();
            eventAggregate.ParticipateInPublicEvent(guestAggregate1.Id);
        }
        var result = eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(EventAggregateErrors.EventCapacityExceeded, result.Errors.First());
    }
    
    // Todo: UC11.F3 need date time
    
    // UC11.F4
    [Fact]
    public void GivenEvent_AndStatusIsActive_AndEventIsPrivate_ThenFailureMessageIsProvided()
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
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(EventAggregateErrors.CantParticipateInPrivateEvent, result.Errors.First());
    }
    
    // UC11.F5
    [Fact]
    public void GivenEvent_AndStatusIsActive_AndEventIsPublic_AndGuestIsAlreadyAParticipant_ThenFailureMessageIsProvided()
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
        var result = eventAggregate.ParticipateInPublicEvent(guestAggregate.Id);
        Assert.Equal(EventAggregateErrors.GuestAlreadyRegistered, result.Errors.First());
    }
    
}