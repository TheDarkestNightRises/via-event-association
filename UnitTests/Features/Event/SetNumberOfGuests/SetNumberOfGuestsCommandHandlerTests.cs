using Microsoft.Extensions.Time.Testing;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.SetNumberOfGuests;

public class SetNumberOfGuestsCommandHandlerTests
{
    private static TimeProvider? _timeProvider;
    private readonly ITestOutputHelper _testOutputHelper;
    private EventAggregate evt;
    private FakeUoW uoW;
    private InMemEventRepoStub evtRepo;
    private SetMaxNumberOfGuestsCommand cmd;
    private ICommandHandler<SetMaxNumberOfGuestsCommand> handler;

    public SetNumberOfGuestsCommandHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
    }

    private void Setup()
    {
         evt = EventFactory.Init()
                    .WithStatus(EventStatus.Draft)
                    .WithTitle(EventTitle.Create("Another title").PayLoad)
                    .WithDescription(EventDescription.Create("Description").PayLoad)
                    .WithVisibility(EventVisibility.Public)
                    .WithCapacity(EventCapacity.Create(25).PayLoad)
                    .WithTimeInterval(EventTimeInterval.Create(
                        new DateTime(2023,8,20,19,0,0), 
                        new DateTime(2023,8,20,21,0,0),
                        _timeProvider).PayLoad)
                    .Build();
        evtRepo = new InMemEventRepoStub();
        evtRepo.Events.Add(evt);
        uoW = new FakeUoW();
        handler = new SetMaxNumberOfGuestsHandler(evtRepo, uoW);
       
    }
    
    [Fact]
    public async Task HandleAsync_ValidCommand_Success()
    {
        // Arrange
        Setup();
        var capacity = 10;
        var command = SetMaxNumberOfGuestsCommand.Create(evt.Id.Id.ToString()!,capacity).PayLoad;
        
        //Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(capacity, (int)evtRepo.Events[0].EventCapacity);
    }
    [Fact]
    public async Task HandleAsync_InvalidCommand_Failure()
    {
        // Arrange
        Setup();
        var capacity = 10;
        var command = SetMaxNumberOfGuestsCommand.Create(evt.Id.Id.ToString()!,capacity);
        evt.MakeEventActive();
        //Act
        var result = await handler.HandleAsync(command.PayLoad);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.NumberOfGuestsCanNotBeReduced, result.Errors.FirstOrDefault());
    }
}