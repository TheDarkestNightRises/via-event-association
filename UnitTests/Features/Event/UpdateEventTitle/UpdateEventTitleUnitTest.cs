using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateEventTitle;

public class UpdateEventTitleUnitTest
{
    //UC2.S1
    [Fact]
    public void GivenAnEventId_WhenCreatorSelectsTheTitleOfTheEvent_ThenTheTitleOfTheEventIsUpdated()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Draft;
        var newUpdatedTitle = "Title is updated now.";
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True((string) eventAggregate.EventTitle == newUpdatedTitle);
        Assert.True(((string) eventAggregate.EventTitle).Length >= 3 && ((string) eventAggregate.EventTitle).Length <= 75);
    }
    
    //UC2.S2
    [Fact]
    public void GivenAnEventWithId_WhenCreatorSelectsTheTitleOfTheEventInReadyStatus_ThenTheTitleOfTheEventIsUpdated()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Ready;
        var newUpdatedTitle = "Title is updated now.";
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True((string) eventAggregate.EventTitle == newUpdatedTitle);
        Assert.True(((string) eventAggregate.EventTitle).Length >= 3 && ((string) eventAggregate.EventTitle).Length <= 75);
    }
    
    //UC2.F1
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithZeroCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Ready;
        var newUpdatedTitle = "";
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }
    
    //UC2.F2
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithLessThenThreeCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Ready;
        var newUpdatedTitle = "xy";
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }
    
    //UC2.F3
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithMoreThenSeventyFiveCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Ready;
        var newUpdatedTitle = "abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk ";
        
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }
    
    //UC2.F4
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithNullCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Ready;
        String newUpdatedTitle = null;
        
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleCanNotBeUpdatedWithNullValue, result.Errors.First());
    }
    
    //UC2.F5
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleInActiveStatus_ThenReturnFailureMessageEventCannotBeModified()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Active;
        String newUpdatedTitle = "active event";
        
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotUpdateTitleOnActiveEvent, result.Errors.First());
    }
    
    //UC2.F6
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleInCancelledStatus_ThenReturnFailureMessageEventCannotBeModified()
    {
        // Arrange  
        var updatedResult = EventAggregate.Create();
        var eventAggregate = updatedResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Cancelled;
        String newUpdatedTitle = "active event";
        
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotUpdateTitleCancelledEvent, result.Errors.First());
    }
    
}