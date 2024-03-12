using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.InviteGuestToEvent;

public class InviteGuestToEventUnitTest
{
    // UC13.s1
    [Fact]
    public void GivenAnExistingEventWithId_WhenTheCreatorInvitesGuest_ThenPendingInvitationStatusIsRegistered()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();

        // Act
        var result = eventAggregate.InviteGuestToEvent(registeredGuest.Id);

        // Assert
        Assert.True(result.IsSuccess);
    }

    // UC13 F1
    [Fact]
    public void GivenEventWithDraftOrCancelledStatus_WhenCreatorInvitesGuest_ShouldRejectWithProperMessage()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();

        // Act
        var result = eventAggregate.InviteGuestToEvent(registeredGuest.Id);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Equal(InvitationErrors.Invitation.CantInviteGuestToDraftOrCancelledEvent, result.Errors.First());
    }
    
    /*// UC13 F2
    [Fact]
    public void GivenActiveEventWithMaximumGuests_WhenCreatorInvitesGuest_ShouldRejectWithProperMessage()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithMaximumGuests(5) // Assuming the maximum allowed guests is 5 for this test
            .WithGuestsAttending(5) // Assuming the event is already full
            .Build();

        var registeredGuest = GuestFactory.Init()
            .Build();

        // Act
        var result = eventAggregate.InviteGuestToEvent(registeredGuest.Id);

        // Assert
        Assert.True(result.Errors.Any();
        Assert.Equal(InvitationErrors.Invitation.CantInviteGuestIfEventIsFull, result.Errors.First());
    }*/
}