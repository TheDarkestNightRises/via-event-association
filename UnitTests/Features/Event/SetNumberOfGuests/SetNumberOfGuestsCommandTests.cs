using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.SetNumberOfGuests;

public class SetNumberOfGuestsCommandTests
{
    [Fact]
    public void Create_ValidInput_ReturnsSuccess()
    {
        // Arrange
        EventId evId = EventId.Create();
        int capacity = 10;
        
        // Act
        var result = SetMaxNumberOfGuestsCommand.Create(evId.Id.ToString(), capacity);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(capacity, (int)result.PayLoad.EventCapacity);
    }
    [Fact]
    public void Create_InvalidInput_ReturnsFailure()
    {
        // Arrange
        EventId evId = EventId.Create();
        int capacity = -10;
        
        // Act
        var result = SetMaxNumberOfGuestsCommand.Create(evId.Id.ToString(), capacity);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.EventCapacityCannotBeNegative, result.Errors[0]);
    }
}