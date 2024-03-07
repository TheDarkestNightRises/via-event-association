using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateEventTitle;

public class UpdateEventTitleUnitTest
{
    //UC2.S1
    [Fact]
    public void GivenDraftEvent_WhenUpdatingTitle_Success()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
    
        var newTitle = new EventTitle("New Event Title");

        // Act
        var result = eventAggregate.UpdateEventTitle(newTitle);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newTitle, eventAggregate.EventTitle);
    }
    
    //UC2.S2
    [Fact]
    public void GivenAnEventWithId_WhenCreatorSelectsTheTitleOfTheEventInReadyStatus_ThenTheTitleOfTheEventIsUpdated()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
    
        var newTitle = new EventTitle("New Event Title");

        // Act
        var result = eventAggregate.UpdateEventTitle(newTitle);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newTitle, eventAggregate.EventTitle);
        //Assert.InRange(EventAggregate.EventTitle.ToString().Length, 3, 75);
    }

    //UC2.F1
    [Fact]
    public void GivenAnEmptyEventTitle_WhenCreatorSelectEventTitleWithZeroCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var newUpdatedTitle = new string("");

        // Act
        var result = EventTitle.Create(newUpdatedTitle);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }

    //UC2.F2
    [Fact]
    public void GivenAnShortTitle_WhenCreatorSelectEventTitleWithLessThenThreeCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange
        var shortTitle = "ab";
        
        // Act
        var result = EventTitle.Create(shortTitle);
    
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }

    //UC2.F3
    [Fact]
    public void GivenLongTitle_WhenCreatorSelectEventTitleWithMoreThenSeventyFiveCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        var longTitle = new string('A', 76);
        
        // Act
        var result = EventTitle.Create(longTitle);
    
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.TitleUpdateInputNotValid, result.Errors.First());
    }

    //UC2.F4
    [Fact]
    public void GivenAnEventWithAnId_WhenCreatorSelectEventTitleWithNullCharacters_ThenReturnFailureMessageIncorrectTitleLength()
    {
        // Arrange  
        string? newUpdatedTitle = null;

        // Act
        var result = EventTitle.Create(newUpdatedTitle);

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
        var result = eventAggregate.UpdateEventTitle(new EventTitle(newUpdatedTitle));
    
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
        var result = eventAggregate.UpdateEventTitle(new EventTitle(newUpdatedTitle));
    
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CanNotUpdateTitleCancelledEvent, result.Errors.First());
    }
    
}