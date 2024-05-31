using Microsoft.Extensions.Time.Testing;
using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Event.CancelParticipationInEvent;

public class CancelParticipationInEventUnitTests
{
    private TimeProvider _timeProvider;

    public CancelParticipationInEventUnitTests()
    {
        // Set the current time in fake time provider in order to test methods which make use of DateTime.Now
        _timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
    }
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0)).PayLoad)
            .Build();
        eventAggregate.ParticipateInPublicEvent(guestAggregate.Id, _timeProvider);
        var initialNumberOfGuests = eventAggregate.EventParticipants.Count;
        eventAggregate.CancelParticipationInEvent(guestAggregate.Id, _timeProvider);
        Assert.Equal(initialNumberOfGuests - 1, eventAggregate.EventParticipants.Count);
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0)).PayLoad)
            .Build();
        eventAggregate.ParticipateInPublicEvent(guestAggregate.Id, _timeProvider);
        var initialNumberOfGuests = eventAggregate.EventParticipants.Count;
        eventAggregate.CancelParticipationInEvent(guestAggregate.Id, _timeProvider);
        Assert.DoesNotContain(guestAggregate.Id, eventAggregate.EventParticipants);
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0)).PayLoad)
            .Build();
        var initialNumberOfGuests = eventAggregate.EventParticipants.Count;
        eventAggregate.ParticipateInPublicEvent(guestAggregate1.Id,_timeProvider);
        eventAggregate.CancelParticipationInEvent(guestAggregate2.Id,_timeProvider);
        Assert.Equal(initialNumberOfGuests + 1, eventAggregate.EventParticipants.Count);
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0)).PayLoad)
            .Build();
        eventAggregate.ParticipateInPublicEvent(guestAggregate1.Id, _timeProvider);
        eventAggregate.CancelParticipationInEvent(guestAggregate2.Id, _timeProvider);
        Assert.DoesNotContain(guestAggregate2.Id, eventAggregate.EventParticipants);
    }
    
    // UC12.F1
    [Fact]
    public void GivenEvent_AndRegisteredGuest_WhenCancelParticipation_AndEventInThePast_ThenFailure()
    {
        var guestAggregate1 = GuestFactory.Init()
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0)).PayLoad)
            .Build();

        eventAggregate.ParticipateInPublicEvent(guestAggregate1.Id, _timeProvider);
        var futureTimeProvider = new FakeTimeProvider(new DateTime(2023, 9, 22, 10, 0, 0));
        var result = eventAggregate.CancelParticipationInEvent(guestAggregate1.Id, futureTimeProvider);
        Assert.True(result.IsFailure);
        Assert.Contains(guestAggregate1.Id, eventAggregate.EventParticipants);
        Assert.Equal(EventAggregateErrors.CanNotCancelParticipationInPastOrOngoingEvent, result.Errors.First());
        
    }
    
}