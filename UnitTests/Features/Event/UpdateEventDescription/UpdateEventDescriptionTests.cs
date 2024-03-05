using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateEventDescription;

public class UpdateEventDescriptionTests
{
    [Fact]
    public void GivenDraftEvent_WhenUpdatingDescription_Success()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        
        var newDescription = new EventDescription("Nullam tempor lacus nisl, eget tempus quam maximus malesuada.");

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventDescription == newDescription);
    }
    
    //UC3.S2
    [Fact]
    public void GivenEvent_WhenUpdatingEmptyDescription_Success()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        var newDescription = new EventDescription("");

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventDescription == newDescription);
    }

    //UC3.S3
    [Fact]
    public void GivenReadyEvent_WhenUpdatingDescription_Success()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();        
        var newDescription = new EventDescription("Nullam tempor lacus nisl, eget tempus quam maximus malesuada.");

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventDescription == newDescription);
    }
    
    //UC3.S3
    [Fact]
    public void GivenReadyEvent_WhenUpdatingDescription_BecomesDraft()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();        
        var newDescription = new EventDescription("Nullam tempor lacus nisl, eget tempus quam maximus malesuada.");

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(eventAggregate.EventStatus == EventStatus.Draft);
    }
    
    //UC3.F1
    // [Fact]
    // public void GivenLongDescription_WhenUpdatingEventDescription_ThenReturnIncorrectLengthError()
    // {
    //     // Arrange
    //     var eventAggregate = EventFactory.Init()
    //         .Build();       
    //     
    //     var longDescription = EventDescription.Create(new string('A', 251));
    //
    //     // Act
    //     var result = eventAggregate.UpdateEventDescription(longDescription);
    //
    //     // Assert
    //     Assert.True(result.IsFailure);
    //     Assert.Equal(EventAggregateErrors.EventDescriptionIncorrectLength, result.Errors.First());
    // }
    //
    [Fact]
    public void GivenLongDescription_WhenCreatingEventDescription_ShouldReturnFailureResultWithIncorrectLengthError()
    {
        // Arrange
        var longDescription = new string('A', 251);

        // Act
        var result = EventDescription.Create(longDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.EventDescriptionIncorrectLength, result.Errors.First());
    }
    
    //UC3.F2
    [Fact]
    public void GivenCanceledEvent_WhenUpdatingEventDescription_ThenReturnCancelledEventCantBeModified()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();       
        var newDescription = new EventDescription("Nullam tempor lacus nisl, eget tempus quam maximus malesuada.");

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.CancelledEventCantBeModified, result.Errors.First());
    }
    
    //UC3.F3
    [Fact]
    public void GivenActiveEvent_WhenUpdatingEventDescription_ThenReturnActiveEventCantBeModified()
    {
        // Arrange
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();       
        var newDescription = new EventDescription("Nullam tempor lacus nisl, eget tempus quam maximus malesuada.");

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.ActiveEventCantBeModified, result.Errors.First());
    }
    
    //Validation for EventDescription
    [Fact]
    public void GivenNullDescription_WhenCreatingEventDescription_ShouldReturnFailureResultWithCantBeNullError()
    {
        // Arrange
        string? nullDescription = null;

        // Act
        var result = EventDescription.Create(nullDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.EventDescriptionCantBeNull, result.Errors.First());
    }

    [Fact]
    public void GivenValidDescription_WhenCreatingEventDescription_ShouldReturnSuccessResultWithValidDescription()
    {
        // Arrange
        var validDescription = "Valid description";

        // Act
        var result = EventDescription.Create(validDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validDescription, result.PayLoad.Description);
    }

}

