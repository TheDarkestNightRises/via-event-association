using FakeItEasy;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.MakeEventPrivate;

public class MakeEventPrivateHandlerTests
{
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;
    private Guid _eventId;
    private readonly MakeEventPrivateCommand cmd;
    private readonly ICommandHandler<MakeEventPrivateCommand> handler;
    private readonly EventVisibility _eventVisibility;

    public MakeEventPrivateHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        cmd = MakeEventPrivateCommand.Create(_eventId.ToString()).PayLoad;
        handler = new MakeEventPrivateCommandHandler(_repositoryMock, _uowMock);
        _eventVisibility = EventVisibility.Private;
    }
    
    [Fact]
    public async Task GivenValidCommand_WhenHandleAsync_ThenSuccess()
    {
        var originalEvent = EventFactory.ValidEvent();
        A.CallTo(() => _repositoryMock.GetAsync(cmd.Id)).Returns(originalEvent); 

        // Act
        var result = await handler.HandleAsync(cmd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_eventVisibility,  originalEvent.EventVisibility); 
    }
    
    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenReturnFailure()
    {
        var originalEvent = EventFactory.CanceledEvent();
        A.CallTo(() => _repositoryMock.GetAsync(cmd.Id)).Returns(originalEvent); 
        
        var result = await handler.HandleAsync(cmd);
        
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.CantMakeCancelledEventPrivate);
    }
}