using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateStartAndEndTime;

public class UpdateStartAndEndTimeTests
{
    private static TimeProvider? _timeProvider;

    public UpdateStartAndEndTimeTests()
    {
        // Set the current time in fake time provider in order to test methoids which make use of DateTime.Now
        _timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
        

    }
    
    // UC4.S1 + S2
    [Theory]//same dates
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,25,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,25,8,0,0},new int[] {2023,8,25,12,15,0})]
    [InlineData(new int[] {2023,8,25,10,0,0},new int[] {2023,8,25,20,0,0})]
    [InlineData(new int[] {2023,8,25,13,0,0},new int[] {2023,8,25,23,0,0})]
    //different dates
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,26,1,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndStatusIsDraft_AndStartTimeIsBeforeEndTime_AndDurationIsValid_AndLocationsAvailable_Success(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
            );
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventTimeInterval == timeInterval);
    }
    // UC4.S3
    [Theory]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,25,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,25,8,0,0},new int[] {2023,8,25,12,15,0})]
    [InlineData(new int[] {2023,8,25,10,0,0},new int[] {2023,8,25,20,0,0})]
    [InlineData(new int[] {2023,8,25,13,0,0},new int[] {2023,8,25,23,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndStatusIsReady_AndStartTimeIsBeforeEndTime_AndDurationIsValid_AndLocationsAvailable_AndSetStatusToDraft_Success(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventTimeInterval == timeInterval);
        Assert.True(eventAggregate.EventStatus == EventStatus.Draft);
    }
    // UC4.S4
    [Theory]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,25,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,25,8,0,0},new int[] {2023,8,25,12,15,0})]
    [InlineData(new int[] {2023,7,20,20,0,0},new int[] {2023,7,20,21,0,0})]
    [InlineData(new int[] {2023,7,20,19,1,0},new int[] {2023,7,20,21,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndStartTimeIsInTheFuture_Success(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventTimeInterval == timeInterval);
        Assert.True(eventAggregate.EventTimeInterval.Start > _timeProvider.GetLocalNow());
    }
    // UC4.S5
    [Theory]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,25,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,25,8,0,0},new int[] {2023,8,25,12,15,0})]
    [InlineData(new int[] {2023,7,20,20,0,0},new int[] {2023,7,20,21,0,0})]
    [InlineData(new int[] {2023,7,20,19,1,0},new int[] {2023,7,20,21,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndEventDurationIsLessThan10Hours_Success(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventTimeInterval == timeInterval);
        Assert.True((eventAggregate.EventTimeInterval.End - eventAggregate.EventTimeInterval.Start) <= new TimeSpan(10,0,0));
    }
    
    // UC4.F1+F2
    [Theory]
    //different dates
    [InlineData(new int[] {2023,8,26,19,0,0},new int[] {2023,8,25,1,0,0})]
    [InlineData(new int[] {2023,8,26,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,27,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,1,8,0,0},new int[] {2023,7,31,12,15,0})]
    //same dates
    [InlineData(new int[] {2023,8,26,19,0,0},new int[] {2023,8,26,0,0,0})]
    [InlineData(new int[] {2023,8,26,16,0,0},new int[] {2023,8,26,0,0,0})]
    [InlineData(new int[] {2023,8,26,19,0,0},new int[] {2023,8,26,18,59,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndEndDateAndTimeIsBeforeStartAndEndTime_Failure(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(eventAggregate.EventTimeInterval);
        Assert.Equal(EventAggregateErrors.EndTimeBeforeStartTime, result.Errors.First());
    }
    // UC4.F3+F4+F9
    [Theory]
    //same dates, less than 1h
    [InlineData(new int[] {2023,8,26,14,0,0},new int[] {2023,8,26,14,50,0})]
    [InlineData(new int[] {2023,8,26,18,0,0},new int[] {2023,8,26,18,59,0})]
    [InlineData(new int[] {2023,8,27,12,0,0},new int[] {2023,8,27,12,30,0})]
    [InlineData(new int[] {2023,8,26,8,0,0},new int[] {2023,8,26,8,0,0})]
    //different dates, less than 1h
    [InlineData(new int[] {2023,8,25,23,30,0},new int[] {2023,8,26,0,15,0})]
    [InlineData(new int[] {2023,8,30,23,1,0},new int[] {2023,8,31,0,0,0})]
    [InlineData(new int[] {2023,8,30,23,59,0},new int[] {2023,8,31,0,1,0})]
    //over 10h
    [InlineData(new int[] {2023,08,30,08,00,00},new int[] {2023,08,30,18,01,00})]
    [InlineData(new int[] {2023,08,30,14,59,00},new int[] {2023,08,31,01,00,00})]
    [InlineData(new int[] {2023,08,30,14,00,00},new int[] {2023,08,31,00,01,00})]
    [InlineData(new int[] {2023,08,30,14,00,00},new int[] {2023,08,31,18,30,00})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndEventDurationIsOutOfBounds_Failure(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(eventAggregate.EventTimeInterval);
        Assert.Equal(EventAggregateErrors.EventDurationOufOfRange, result.Errors.First());
    }
    
    // UC4.F5+F6+F11
    [Theory]
    //start time before 8
    [InlineData(new int[] {2023,08,25,07,50,00},new int[] {2023,08,25,14,00,00})]
    [InlineData(new int[] {2023,08,25,07,59,00},new int[] {2023,08,25,15,00,00})]
    [InlineData(new int[] {2023,08,25,01,01,00},new int[] {2023,08,25,08,30,00})]
    [InlineData(new int[] {2023,08,25,05,59,00},new int[] {2023,08,25,07,59,00})]
    [InlineData(new int[] {2023,08,25,00,59,00},new int[] {2023,08,25,07,59,00})]
    //end time after 1
    [InlineData(new int[] {2023,08,24,23,50,00},new int[] {2023,08,25,01,01,00})]
    [InlineData(new int[] {2023,08,24,22,00,00},new int[] {2023,08,25,07,59,00})]
    [InlineData(new int[] {2023,08,30,23,00,00},new int[] {2023,08,31,02,30,00})]
    //start time before 1 and end time after 8
    [InlineData(new int[] {2023,08,31,00,30,00},new int[] {2023,08,31,08,30,00})]
    [InlineData(new int[] {2023,08,30,23,59,00},new int[] {2023,08,31,08,01,00})]
    [InlineData(new int[] {2023,08,31,01,00,00},new int[] {2023,08,31,08,00,00})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_TimeIntervalOutsideOfAvailableRange_Failure(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(eventAggregate.EventTimeInterval);
        Assert.Equal(EventAggregateErrors.TimeIntervalUnavailable, result.Errors.First()); 
    }
    // UC4.F7
    [Theory]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,25,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,25,8,0,0},new int[] {2023,8,25,12,15,0})]
    [InlineData(new int[] {2023,8,25,10,0,0},new int[] {2023,8,25,20,0,0})]
    [InlineData(new int[] {2023,8,25,13,0,0},new int[] {2023,8,25,23,0,0})]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,26,1,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndEventIsActive_Failure(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(eventAggregate.EventTimeInterval);
        Assert.Equal(EventAggregateErrors.CanNotChangeTimeOfActiveEvent, result.Errors.First()); 
    }
    
    // UC4.F8
    [Theory]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,25,23,59,0})]
    [InlineData(new int[] {2023,8,25,12,0,0},new int[] {2023,8,25,16,30,0})]
    [InlineData(new int[] {2023,8,25,8,0,0},new int[] {2023,8,25,12,15,0})]
    [InlineData(new int[] {2023,8,25,10,0,0},new int[] {2023,8,25,20,0,0})]
    [InlineData(new int[] {2023,8,25,13,0,0},new int[] {2023,8,25,23,0,0})]
    [InlineData(new int[] {2023,8,25,19,0,0},new int[] {2023,8,26,1,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndEventIsCancelled_Failure(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(eventAggregate.EventTimeInterval);
        Assert.Equal(EventAggregateErrors.CanNotChangeTimeOfCancelledEvent, result.Errors.First()); 
    }
    
    //UC4.F10
    [Theory]
    [InlineData(new int[] {2023,6,25,19,0,0},new int[] {2023,6,25,23,59,0})]
    [InlineData(new int[] {2023,7,19,12,0,0},new int[] {2023,7,19,16,30,0})]
    [InlineData(new int[] {2023,7,20,19,0,0},new int[] {2023,7,20,21,0,0})]
    [InlineData(new int[] {2023,7,20,18,59,0},new int[] {2023,7,20,21,0,0})]
    public void GivenEvent_WhenUpdatingStartAndEndTime_AndStartTimeIsInThePaste_Success(int[] start, int[] end)
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .Build();
        
        var timeInterval = new EventTimeInterval(
            new DateTime(start[0],start[1],start[2],start[3],start[4],start[5]), 
            new DateTime(end[0],end[1],end[2],end[3],end[4],end[5]),
            _timeProvider
        );
        
        // Act
        var result = eventAggregate.UpdateEventTimeInterval(timeInterval);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(eventAggregate.EventTimeInterval);
        Assert.Equal(EventAggregateErrors.EventInThePast, result.Errors.First());
    }
}