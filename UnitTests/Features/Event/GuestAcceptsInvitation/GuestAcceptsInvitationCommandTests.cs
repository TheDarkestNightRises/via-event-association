using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;

namespace UnitTests.Features.Event.GuestAcceptsInvitation;

public class GuestAcceptsInvitationCommandTests
{
    [Fact]
    public void GivenValidEventAndGuestId_WhenCreatingCommand_ThenSuccess()
    {
        var guid = Guid.NewGuid();
        var result = GuestAcceptsInvitationCommand.Create(guid.ToString(), guid.ToString());
        var command = result.PayLoad;

        Assert.True(result.IsSuccess);
        Assert.True(command.EventId.Id == guid );
        Assert.True(command.GuestId.Id == guid );
    }
    
    [Fact]
    public void GivenInvalidEventAndGuestId_WhenCreatingCommand_ThenFailure()
    {
        var guid = "invalid";
        var result = GuestAcceptsInvitationCommand.Create(guid, guid);
        var command = result.PayLoad;

        Assert.True(result.IsFailure);
        Assert.Contains(EventAggregateErrors.InvalidId, result.Errors);
    }
}