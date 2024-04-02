using FakeItEasy;
using Microsoft.Extensions.Time.Testing;
using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.InvitationErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.InviteGuestToEvent;

public class InviteGuestToEventHandlerTests
{
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;
    private Guid _eventId;
    private static TimeProvider? _timeProvider;
    private IGuestRepository _guestRepositoryMock;
    private Guid _guestId;

    public InviteGuestToEventHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _guestRepositoryMock = A.Fake<IGuestRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        _timeProvider = new FakeTimeProvider(new DateTime(2023, 7, 20, 19, 0, 0));
        _guestId = Guid.NewGuid();
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandleAsync_ThenSuccess()
    {
        // Arrange
        var command = GuestIsInvitedToEventCommand.Create(_eventId.ToString(), _guestId.ToString()).PayLoad;

        var originalEvent = EventFactory.Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023, 8, 20, 19, 0, 0),
                new DateTime(2023, 8, 20, 21, 0, 0),
                _timeProvider).PayLoad)
            .Build();
        A.CallTo(() => _repositoryMock.GetAsync(command.EventId)).Returns(originalEvent);
        A.CallTo(() => _guestRepositoryMock.GetAsync(command.GuestId)).Returns(GuestFactory.ValidGuest());
        var handler = new GuestIsInvitedToEventCommandHandler(_repositoryMock, _guestRepositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenFailure()
    {
        // Arrange
        var command =  GuestIsInvitedToEventCommand.Create(_eventId.ToString(), _guestId.ToString()).PayLoad;

        var originalEvent = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();


        A.CallTo(() => _repositoryMock.GetAsync(command.EventId)).Returns(originalEvent);
        A.CallTo(() => _guestRepositoryMock.GetAsync(command.GuestId)).Returns(GuestFactory.ValidGuest());
        var handler = new GuestIsInvitedToEventCommandHandler(_repositoryMock, _guestRepositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(InvitationErrors.Invitation.CantInviteGuestToDraftOrCancelledEvent, result.Errors.First());
    }
}