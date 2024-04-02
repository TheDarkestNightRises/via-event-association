using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.MakeEventPrivate;

public class MakeEventPrivateCommandTests
{
    [Fact]
    public void GivenNothing_WhenCreatingCommand_Success()
    {
        var guid = Guid.NewGuid().ToString();
        var result = MakeEventPrivateCommand.Create(guid);
        Assert.True(result.IsSuccess);
    }

}