using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.CreateNewEvent;

public class CreateNewEventCommandTests
{
    [Fact]
    public void GivenNothing_WhenCreatingCommand_Success()
    {
        var result = CreateNewEventCommand.Create();
        var command = result.PayLoad;
        
        Assert.True(result.IsSuccess);
        Assert.NotNull(command.Id);
        Assert.NotEmpty(command.Id.ToString()!);
    }
    
}