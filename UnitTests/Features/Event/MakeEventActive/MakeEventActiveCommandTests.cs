using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventActive;

public class MakeEventActiveCommandTests
{
    [Fact]
    public void GivenValidEventId_WhenCreatingCommand_ThenSuccess()
    {
        var guid = Guid.NewGuid();
        var result = CreatorActivatesAnEventCommand.Create(guid.ToString());
        var command = result.PayLoad;

        Assert.True(result.IsSuccess);
        Assert.True(command.Id.Id == guid);
    }
    
    [Fact]
    public void GivenInvalidEventId_WhenCreatingCommand_ThenFailure()
    {
        var guid = "invalid";
        var result = CreatorActivatesAnEventCommand.Create(guid);
        var command = result.PayLoad;

        Assert.True(result.IsFailure);
        Assert.Contains(EventAggregateErrors.InvalidId, result.Errors);
    }
}