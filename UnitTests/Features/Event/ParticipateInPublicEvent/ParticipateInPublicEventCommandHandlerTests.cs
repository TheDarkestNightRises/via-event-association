using Microsoft.Extensions.Time.Testing;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.ParticipateInPublicEvent;

public class ParticipateInPublicEventCommandHandlerTests
{
    private TimeProvider _timeProvider;
    private EventAggregate evt;
    private FakeUoW uoW;
    private InMemEventRepoStub evtRepo;
    private SetMaxNumberOfGuestsCommand cmd;
    private ICommandHandler<ParticipateInPublicEventCommand> handler;

    public ParticipateInPublicEventCommandHandlerTests()
    {
        _timeProvider = new FakeTimeProvider(new DateTime(2023, 7, 20, 19, 0, 0));
    }

    private void Setup()
    {
        evt = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023, 8, 20, 19, 0, 0),
                new DateTime(2023, 8, 20, 21, 0, 0)).PayLoad)
            .Build();
        evtRepo = new InMemEventRepoStub();
        evtRepo.Events.Add(evt);
        uoW = new FakeUoW();
        handler = new ParticipateInPublicEventCommandHandler(evtRepo, uoW, _timeProvider);
    }
    
    [Fact]
    public async Task HandleAsync_ValidCommand_Success()
    {
        // Arrange
        Setup();
        var guestId = GuestId.Create();
        var command = ParticipateInPublicEventCommand.Create(evt.Id.Id.ToString()!,guestId.Id.ToString()).PayLoad;
        
        //Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(evt.EventParticipants);
    }
    
    [Fact]
    public async Task HandleAsync_InvalidCommand_Failure()
    {
        // Arrange
        Setup();
        var guestId = GuestId.Create();
        var command = ParticipateInPublicEventCommand.Create(evt.Id.Id.ToString()!,guestId.Id.ToString()).PayLoad;
        var futureTimeProvider = new FakeTimeProvider(new DateTime(2023, 9, 20, 19, 0, 0));

        //Act
        var futureHandler = new ParticipateInPublicEventCommandHandler(evtRepo, uoW, futureTimeProvider);
        var result = await futureHandler.HandleAsync(command);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotParticipateInPastEvent, result.Errors.FirstOrDefault());
    }
    
}