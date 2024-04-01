using FakeItEasy;
using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.MakeEventActive;

public class MakeEventActiveHandlerTests
{
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;
    private Guid _eventId;
    private static TimeProvider? _timeProvider;

    public MakeEventActiveHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        _timeProvider = new FakeTimeProvider(new DateTime(2023, 7, 20, 19, 0, 0));
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandleAsync_ThenSuccess()
    {
        // Arrange
        var command = CreatorActivatesAnEventCommand.Create(_eventId.ToString()).PayLoad;

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
        A.CallTo(() => _repositoryMock.GetAsync(command.Id)).Returns(originalEvent);
        var handler = new CreatorActivatesAnEventCommandHandler(_repositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(originalEvent.EventStatus == EventStatus.Active);
    }

    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenFailure()
    {
        // Arrange
        var command = CreatorActivatesAnEventCommand.Create(_eventId.ToString()).PayLoad;

        var originalEvent = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();


        A.CallTo(() => _repositoryMock.GetAsync(command.Id)).Returns(originalEvent);
        var handler = new CreatorActivatesAnEventCommandHandler(_repositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CancelledEventCantBeActivated, result.Errors.First());
    }
}