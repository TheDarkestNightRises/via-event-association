using FakeItEasy;
using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.MakeEventReady;

public class MakeEventReadyCommandHandlerTests
{
    private IEventRepository _repositoryMock;
    private IUnitOfWork _uowMock;
    private Guid _eventId;
    private readonly MakeEventReadyCommand cmd;
    private readonly ICommandHandler<MakeEventReadyCommand> handler;
    private readonly EventStatus _eventStatus;

    public MakeEventReadyCommandHandlerTests()
    {
        _repositoryMock = A.Fake<IEventRepository>();
        _uowMock = A.Fake<IUnitOfWork>();
        _eventId = Guid.NewGuid();
        cmd = MakeEventReadyCommand.Create(_eventId.ToString()).PayLoad;
        handler = new MakeEventReadyCommandHandler(_repositoryMock, _uowMock);
        _eventStatus = EventStatus.Ready;
    }
    
    [Fact]
    public async Task GivenValidCommand_WhenHandleAsync_ThenSuccess()
    {
        var timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
        var originalEvent = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                timeProvider).PayLoad)
            .Build();
        A.CallTo(() => _repositoryMock.GetAsync(cmd.Id)).Returns(originalEvent); 

        // Act
        var result = await handler.HandleAsync(cmd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_eventStatus,  originalEvent.EventStatus); 
    }
    
    [Fact]
    public async Task GivenInvalidCommand_WhenHandleAsync_ThenReturnFailure()
    {
        var timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
        var originalEvent = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.None)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                timeProvider).PayLoad)
            .Build();
        A.CallTo(() => _repositoryMock.GetAsync(cmd.Id)).Returns(originalEvent); 
        
        var result = await handler.HandleAsync(cmd);
        
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.CanNotReadyAnEventWithNoVisibility);
    }
}