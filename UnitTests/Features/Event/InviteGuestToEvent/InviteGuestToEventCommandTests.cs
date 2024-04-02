using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;

namespace UnitTests.Features.Event.InviteGuestToEvent;

public class InviteGuestToEventCommandTests
{
    [Fact]
    public void GivenValidEventId_WhenCreatingCommand_ThenSuccess()
    {
        var guid = Guid.NewGuid();
        var result = GuestIsInvitedToEventCommand.Create(guid.ToString(), guid.ToString());
        var command = result.PayLoad;

        Assert.True(result.IsSuccess);
        Assert.True(command.EventId.Id == guid );
        Assert.True(command.GuestId.Id == guid );
    }
    
    [Fact]
    public void GivenInvalidEventId_WhenCreatingCommand_ThenFailure()
    {
        var guid = "invalid";
        var result = GuestIsInvitedToEventCommand.Create(guid, guid);
        var command = result.PayLoad;

        Assert.True(result.IsFailure);
        Assert.Contains(EventAggregateErrors.InvalidId, result.Errors);
    }
    
    
}