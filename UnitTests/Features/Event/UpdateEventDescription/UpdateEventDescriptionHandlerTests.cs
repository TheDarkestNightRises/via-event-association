using FakeItEasy;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.UpdateEventDescription;

public class UpdateEventDescriptionHandlerTests
{
    private readonly IEventRepository _repositoryMock;
    private readonly IUnitOfWork _uowMock;
    private readonly Guid _eventId;
    private readonly UpdateEventDescriptionCommand cmd;
    private readonly ICommandHandler<UpdateEventDescriptionCommand> handler;
    private readonly string description;

    public UpdateEventDescriptionHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        description = "Updated description";
        cmd = UpdateEventDescriptionCommand.Create(_eventId.ToString(), description).PayLoad;
        handler = new UpdateEventDescriptionCommandHandler(_repositoryMock, _uowMock);
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
        Assert.Equal(description, (string) originalEvent.EventDescription); 
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
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.CancelledEventCantBeModified);
    }

}