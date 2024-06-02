using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.GuestAcceptsInvitation;

public class GuestAcceptsInvitationUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public GuestAcceptsInvitationUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    // UC14.s1
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredInvitedGuest_WhenGuestAcceptsInvitation_InvitationStatusIsSetToAccepted()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithCapacity(new EventCapacity(3))
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();

        // Act
        eventAggregate.InviteGuestToEvent(registeredGuest.Id);
        eventAggregate.GuestAcceptsInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationStatus.Accepted, eventAggregate.Invitations[0].InvitationStatus);
    }
    
    // UC14.f1
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredNotInvitedGuest_WhenGuestAcceptsInvitation_DisplayGuestHasNotBeenInvited()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithCapacity(new EventCapacity(3))
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();

        // Act
        var result = eventAggregate.GuestAcceptsInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationErrors.Invitation.GuestHasNotBeenInvitedToTheEvent, result.Errors.FirstOrDefault());    
    }
    
    // UC14.f2
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredInvitedGuest_AndCapacityIsFull_WhenGuestAcceptsInvitation_DisplayCapacityIsFull()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithCapacity(new EventCapacity(1))
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();
        
        var registeredGuest2 = GuestFactory.Init()
            .Build();
        
        // Act
        eventAggregate.InviteGuestToEvent(registeredGuest.Id);
        eventAggregate.InviteGuestToEvent(registeredGuest2.Id);
        eventAggregate.GuestAcceptsInvitation(registeredGuest2.Id);

        var result = eventAggregate.GuestAcceptsInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationErrors.Invitation.EventShouldNotBeFullToAcceptInvitation, result.Errors.FirstOrDefault());    
    }
    
    // UC14.f3
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredInvitedGuest_AndEventIsNotActive_WhenGuestAcceptsInvitation_DisplayEventMustBeActiveToAccept()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithCapacity(new EventCapacity(1))
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();
        
        // Act
        eventAggregate.InviteGuestToEvent(registeredGuest.Id);
        var result = eventAggregate.GuestAcceptsInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationErrors.Invitation.EventMustBeActiveToAcceptInvitation, result.Errors.FirstOrDefault());    
    }
}