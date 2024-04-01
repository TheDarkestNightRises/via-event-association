using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

namespace UnitTests.Features.Event.MakeEventReady;

public class MakeEventReadyCommandTests
{
    [Fact]
    public void GivenNothing_WhenCreatingCommand_Success()
    {
        var guid = Guid.NewGuid().ToString();
        var result = MakeEventReadyCommand.Create(guid);
        Assert.True(result.IsSuccess);
    }
}