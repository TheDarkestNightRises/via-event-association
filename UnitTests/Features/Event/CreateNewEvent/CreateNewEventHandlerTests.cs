using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.CreateNewEvent;

public class CreateNewEventHandlerTests
{
    private EventAggregate evt;
    private FakeUoW uoW;
    private InMemEventRepoStub evtRepo;
    private CreateNewEventCommand cmd;
    private ICommandHandler<CreateNewEventCommand> handler;
    
    private void Setup()
    {
        evtRepo = new InMemEventRepoStub();
        uoW = new FakeUoW();
        handler = new CreateNewEventCommandHandler(evtRepo, uoW);
        cmd = CreateNewEventCommand.Create().PayLoad;
    }

    [Fact]
    public async Task GivenNothing_WhenCreatingEvent_ThenNewEventIsCreatedWithDefaultValues()
    {
        Setup();

        var result = await handler.HandleAsync(cmd);
        
        Assert.True(result.IsSuccess);
        Assert.Single(evtRepo.Events);
    }
}