using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.GuestDeclinesInvitation;

public class GuestDeclinesInvitationUnitTests
{
    // UC15.s1
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredInvitedGuest_WhenGuestDeclinesInvitation_InvitationStatusIsSetToDeclined()
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
        eventAggregate.GuestDeclinesInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationStatus.Declined, eventAggregate.Invitations[0].InvitationStatus);
    }
    
    // UC15.s2
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredInvitedGuest_WhenGuestDeclinesInvitation_ButInvitationIsAccepted_InvitationStatusIsSetToDeclined()
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
        eventAggregate.GuestAcceptsInvitation(registeredGuest.Id);
        eventAggregate.GuestDeclinesInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationStatus.Declined, eventAggregate.Invitations[0].InvitationStatus);
    }
    
    // UC15.f1
    [Fact]
    public void GivenAnExistingEventWithId_AndARegisteredNotInvitedGuest_WhenGuestDeclinesInvitation_DisplayGuestHasNotBeenInvited()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithCapacity(new EventCapacity(1))
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();
        
        // Act
        var result = eventAggregate.GuestDeclinesInvitation(registeredGuest.Id);
        
        // Assert
        Assert.Equal(InvitationErrors.Invitation.GuestHasNotBeenInvitedToTheEvent, result.Errors.FirstOrDefault());    
    }
}