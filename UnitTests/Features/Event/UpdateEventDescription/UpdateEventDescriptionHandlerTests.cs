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
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;

    public UpdateEventDescriptionHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandleAsync_ThenSuccess()
    {
        // Arrange
        var updatedDescription = "Updated description";
        var eventId = Guid.NewGuid().ToString();
        var command = UpdateEventDescriptionCommand.Create(eventId, updatedDescription).PayLoad;
        
        var originalEvent = EventFactory.ValidEvent();
        A.CallTo(() => _repositoryMock.GetAsync(command.Id)).Returns(originalEvent); 
        var handler = new UpdateEventDescriptionCommandHandler(_repositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(updatedDescription, (string) originalEvent.EventDescription); 
    }
    
    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenReturnFailure()
    {
        // Arrange
        var updatedDescription = "Updated description";
        var eventId = Guid.NewGuid();
        var command = UpdateEventDescriptionCommand.Create(eventId.ToString(), updatedDescription).PayLoad;
        
        var originalEvent = EventFactory.CanceledEvent();
        A.CallTo(() => _repositoryMock.GetAsync(new EventId(eventId))).Returns(originalEvent); 
        var handler = new UpdateEventDescriptionCommandHandler(_repositoryMock, _uowMock);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.CancelledEventCantBeModified);
    }

}