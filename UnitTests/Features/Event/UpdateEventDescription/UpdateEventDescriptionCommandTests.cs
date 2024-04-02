using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateEventDescription;

public class UpdateEventDescriptionCommandTests
{
    [Fact]
    public void GivenValidDescription_WhenCreatingUpdateEventDescriptionCommand_ThenSuccess()
    {
        var description = "Event is cool";
        var guid = Guid.NewGuid().ToString();
        var result = UpdateEventDescriptionCommand.Create(guid, description);
        var command = result.PayLoad;

        Assert.True(result.IsSuccess);
        Assert.True((string) command.Description == description);
    }


    [Fact]
    public void GivenInvalidDescription_WhenCreatingCommand_ThenErrorIsProvided()
    {
        var longDescription = new string('A', 251);
        var guid = Guid.NewGuid().ToString();
        var result = UpdateEventDescriptionCommand.Create(guid, longDescription);
        var command = result.PayLoad;

        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == EventAggregateErrors.EventDescriptionIncorrectLength);
    }
}