using FakeItEasy;
using Microsoft.Extensions.Time.Testing;
using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.GuestDeclinesInvitation;

public class GuestDeclinesInvitationCommandHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;
    private Guid _eventId;
    private static TimeProvider? _timeProvider;
    private IGuestRepository _guestRepositoryMock;
    private Guid _guestId;

    public GuestDeclinesInvitationCommandHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
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
        var command = GuestDeclinesInvitationCommand.Create(_eventId.ToString(), _guestId.ToString()).PayLoad;
        
        var originalEvent = EventFactory.Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Active)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023, 8, 20, 19, 0, 0),
                new DateTime(2023, 8, 20, 21, 0, 0)).PayLoad)
            .Build();
        var registeredGuest = GuestFactory.Init()
            .Build();
        originalEvent.InviteGuestToEvent(registeredGuest.Id);
        A.CallTo(() => _repositoryMock.GetAsync(command.EventId)).Returns(originalEvent);
        A.CallTo(() => _guestRepositoryMock.GetAsync(command.GuestId)).Returns(registeredGuest);
        var handler = new GuestDeclinesInvitationCommandHandler(_repositoryMock, _guestRepositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);
        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenFailure()
    {
        // Arrange
        var command = GuestDeclinesInvitationCommand.Create(_eventId.ToString(), _guestId.ToString()).PayLoad;
        
        var originalEvent = EventFactory.Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Cancelled)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023, 8, 20, 19, 0, 0),
                new DateTime(2023, 8, 20, 21, 0, 0)).PayLoad)
            .Build();
        var registeredGuest = GuestFactory.Init()
            .Build();
        originalEvent.InviteGuestToEvent(registeredGuest.Id);
        A.CallTo(() => _repositoryMock.GetAsync(command.EventId)).Returns(originalEvent);
        A.CallTo(() => _guestRepositoryMock.GetAsync(command.GuestId)).Returns(registeredGuest);
        var handler = new GuestDeclinesInvitationCommandHandler(_repositoryMock, _guestRepositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);
        // Assert
        Assert.True(result.IsFailure);
    }
}