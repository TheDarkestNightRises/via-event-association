using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateStartAndEndTime;

public class UpdateStartAndEndTimeCommandTests
{
    
    [Fact]
    public void Create_ValidInput_ReturnsSuccess()
    {
        // Arrange
        EventId evId = EventId.Create();
        DateTime start = DateTime.UtcNow.AddHours(1); //TODO: switch to actual dates when time provider fixed
        DateTime end = start.AddHours(2);
        
        // Act
        var result = UpdateTimeIntervalCommand.Create(evId.Id.ToString(), start, end);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(evId, result.PayLoad.Id);
        Assert.Equal(start, result.PayLoad.EventTimeInterval.Start);
        Assert.Equal(end, result.PayLoad.EventTimeInterval.End);
    }
    [Fact]
    public void Create_InvalidInput_ReturnsFailure()
    {
        // Arrange
        EventId evId = EventId.Create();
        DateTime start = DateTime.UtcNow.AddHours(2);//TODO: switch to actual dates when time provider fixed
        DateTime end = start.AddHours(-1);
        
        // Act
        var result = UpdateTimeIntervalCommand.Create(evId.Id.ToString(), start, end);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.EndTimeBeforeStartTime, result.Errors[0]);
    }
}