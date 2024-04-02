using Microsoft.Extensions.Time.Testing;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

using Xunit.Abstractions;

namespace UnitTests.Features.Event.UpdateStartAndEndTime;

public class UpdateStartAndEndTimeCommandHandlerTests
{
    private static TimeProvider? _timeProvider;
    private readonly ITestOutputHelper _testOutputHelper;
    private EventAggregate evt;
    private FakeUoW uoW;
    private InMemEventRepoStub evtRepo;
    private UpdateTimeIntervalCommand cmd;
    private ICommandHandler<UpdateTimeIntervalCommand> handler;

    public UpdateStartAndEndTimeCommandHandlerTests(ITestOutputHelper testOutputHelper)
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
        handler = new UpdateTimeIntervalHandler(evtRepo, uoW);
       
    }
    
    [Fact]
    public async Task HandleAsync_ValidCommand_Success()
    {
        // Arrange
        Setup();
        var start = DateTime.UtcNow.AddHours(1);
        var end = start.AddHours(2);
        var command = UpdateTimeIntervalCommand.Create(evt.Id.Id.ToString()!, start, end, _timeProvider).PayLoad;
        
        //Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(start, evtRepo.Events[0].EventTimeInterval!.Start);
    }
    [Fact]
    public async Task HandleAsync_InvalidCommand_Failure()
    {
        // Arrange
        Setup();
        var start = DateTime.UtcNow.AddHours(1);
        var end = start.AddHours(2);
        var command = UpdateTimeIntervalCommand.Create(evt.Id.Id.ToString()!, start, end, _timeProvider);
        evt.MakeEventActive();
        //Act
        var result = await handler.HandleAsync(command.PayLoad);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotChangeTimeOfActiveEvent, result.Errors.FirstOrDefault());
    }
}
