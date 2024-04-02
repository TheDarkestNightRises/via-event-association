using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

namespace UnitTests.Features.Event.MakeEventPublicUnitTests;

public class MakeEventPublicCommandTests
{
    [Fact]
    public void GivenNothing_WhenCreatingCommand_Success()
    {
        var guid = Guid.NewGuid().ToString();
        var result = MakeEventPublicCommand.Create(guid);
        Assert.True(result.IsSuccess);
    }
}