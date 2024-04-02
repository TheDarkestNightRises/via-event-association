using FakeItEasy;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.MakeEventPublicUnitTests;

public class MakeEventPublicHandlerTests
{
    private readonly IEventRepository _repositoryMock;
    private readonly IUnitOfWork _uowMock;
    private readonly Guid _eventId;
    private readonly MakeEventPublicCommand cmd;
    private readonly ICommandHandler<MakeEventPublicCommand> handler;
    private readonly EventVisibility _eventVisibility;



    public MakeEventPublicHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        cmd = MakeEventPublicCommand.Create(_eventId.ToString()).PayLoad;
        handler = new MakeEventPublicCommandHandler(_repositoryMock, _uowMock);
        _eventVisibility = EventVisibility.Public;
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
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.CantMakeCancelledEventPublic);
    }

}