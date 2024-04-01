using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Event.ParticipateInPublicEvent;

public class ParticipateInPublicEventCommandTests
{
    [Fact]
    public void GivenNothing_WhenCreatingCommand_Success()
    {
        EventId evId = EventId.Create();
        GuestId gId = GuestId.Create();
        
        var result = ParticipateInPublicEventCommand.Create(evId.Id.ToString(), gId.Id.ToString());
        
        Assert.True(result.IsSuccess);
        Assert.Equal(evId, result.PayLoad.Id);
        Assert.Equal(gId, result.PayLoad.GuestId);
    }
    
    [Fact]
    public void Create_InvalidInput_ReturnsFailure()
    {
        EventId evId = EventId.Create();
        
        var result = ParticipateInPublicEventCommand.Create(evId.Id.ToString(), "wrong_id");
        
        Assert.True(result.IsFailure);
        Assert.Equal(GuestAggregateErrors.Id.InvalidId, result.Errors[0]);
    }
}