using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Event.CancelParticipationInEvent;

public class CancelParticipationInEventCommandTests
{
    [Fact]
    public void Create_ValidInput_ReturnsSuccess()
    {
        // Arrange
        EventId evId = EventId.Create();
        GuestId gId = GuestId.Create();
        
        // Act
        var result = CancelParticipationInEventCommand.Create(evId.Id.ToString(), gId.Id.ToString());
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(evId, result.PayLoad.Id);
        Assert.Equal(gId, result.PayLoad.GuestId);
    }
    [Fact]
    public void Create_InvalidInput_ReturnsFailure()
    {
        // Arrange
        EventId evId = EventId.Create();
        
        // Act
        var result = CancelParticipationInEventCommand.Create(evId.Id.ToString(), "wrong_id");
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(GuestAggregateErrors.Id.InvalidId, result.Errors[0]);
    }
}