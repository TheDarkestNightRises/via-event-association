using FakeItEasy;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.UpdateEventTitle;

public class UpdateEventTitleHandlerTests
{
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;
    private Guid _eventId;
    private UpdateEventTitleCommand cmd;
    private ICommandHandler<UpdateEventTitleCommand> handler;
    private string title;

    public UpdateEventTitleHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        title = "Updated title";
        cmd = UpdateEventTitleCommand.Create(_eventId.ToString(), title).PayLoad;
        handler = new UpdateEventTitleHandler(_repositoryMock, _uowMock);
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandleAsync_ThenSuccess()
    {
        // Arrange
        var originalEvent = EventFactory.ValidEvent();
        A.CallTo(() => _repositoryMock.GetAsync(cmd.Id)).Returns(originalEvent); 

        // Act
        var result = await handler.HandleAsync(cmd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(title, (string) originalEvent.EventTitle); 
    }

    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenReturnFailure()
    {
        // Arrange
        var originalEvent = EventFactory.CanceledEvent();
        A.CallTo(() => _repositoryMock.GetAsync(cmd.Id)).Returns(originalEvent); 

        // Act
        var result = await handler.HandleAsync(cmd);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.CanNotUpdateTitleCancelledEvent);
    }

}