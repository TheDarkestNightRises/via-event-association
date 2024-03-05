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
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        var newUpdatedTitle = new EventTitle("Title is updated now.");
        
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventTitle == newUpdatedTitle);
    }
    
    //UC2.S2
    [Fact]
    public void GivenAnEventWithId_WhenCreatorSelectsTheTitleOfTheEventInReadyStatus_ThenTheTitleOfTheEventIsUpdated()
    {
        // Arrange  
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        
        var newUpdatedTitle = new EventTitle("Title is updated now.");
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventTitle == newUpdatedTitle);
        Assert.True(((string) eventAggregate.EventTitle).Length >= 3 && ((string) eventAggregate.EventTitle).Length <= 75);
    }

    //UC2.F1
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithZeroCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
    
        var newUpdatedTitle = new EventTitle(""); // Use the EventTitle constructor for consistency
    
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }

    //UC2.F2
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithLessThenThreeCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();

        var newUpdatedTitle = new EventTitle ("xy");
    
        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);
    
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }

    //UC2.F3
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithMoreThenSeventyFiveCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();

        var longTitle = new EventTitle ("abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk abcdefghlk ");
    
        // Act
        var result = eventAggregate.UpdateEventTitle(longTitle);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }

    //UC2.F4
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithNullCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange
        string? nullUpdatedTitle = null;


        // Act
        var result = EventAggregate.UpdateEventTitle(nullUpdatedTitle);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleCanNotBeUpdatedWithNullValue, result.Errors.First());
    }

    //UC2.F5
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleInActiveStatus_ThenReturnFailureMessageEventCannotBeModified()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();

        string newUpdatedTitle = "active event";

        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotUpdateTitleOnActiveEvent, result.Errors.First());
    }

    //UC2.F6
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleInCancelledStatus_ThenReturnFailureMessageEventCannotBeModified()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();

        string newUpdatedTitle = "active event";

        // Act
        var result = eventAggregate.UpdateEventTitle(newUpdatedTitle);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotUpdateTitleCancelledEvent, result.Errors.First());
    }
    
}