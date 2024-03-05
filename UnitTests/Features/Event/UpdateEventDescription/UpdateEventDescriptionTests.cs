using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateEventDescription;

public class UpdateEventDescriptionTests
{
    //UC3.S1
    [Fact]
    public void GivenDraftEvent_WhenUpdatingDescription_Success()
    {
        // Arrange
        var createdResult = EventAggregate.Create();
        var eventAggregate = createdResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Draft;
        var newDescription = "Nullam tempor lacus nisl, eget tempus quam maximus malesuada.";

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True((string) eventAggregate.EventDescription == newDescription);
    }
    
    //UC3.S2
    [Fact]
    public void GivenEvent_WhenUpdatingEmptyDescription_Success()
    {
        // Arrange
        var createdResult = EventAggregate.Create();
        var eventAggregate = createdResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Draft;
        var newDescription = "";

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True((string) eventAggregate.EventDescription == newDescription);
    }

    //UC3.S3
    [Fact]
    public void GivenReadyEvent_WhenUpdatingDescription_Success()
    {
        // Arrange
        var createdResult = EventAggregate.Create();
        var eventAggregate = createdResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Ready;
        var newDescription = "Nullam tempor lacus nisl, eget tempus quam maximus malesuada.";

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True((string) eventAggregate.EventDescription == newDescription);
    }
    
    //UC3.F1
    [Fact]
    public void GivenLongDescription_WhenUpdatingEventDescription_ThenReturnIncorrectLengthError()
    {
        // Arrange
        var createdResult = EventAggregate.Create();
        var eventAggregate = createdResult.PayLoad;
        var longDescription = new string('A', 251);

        // Act
        var result = eventAggregate.UpdateEventDescription(longDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.EventDescriptionIncorrectLength, result.Errors.First());
    }
    
    //UC3.F2
    [Fact]
    public void GivenCanceledEvent_WhenUpdatingEventDescription_ThenReturnCancelledEventCantBeModified()
    {
        // Arrange
        var createdResult = EventAggregate.Create();
        var eventAggregate = createdResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Cancelled;
        var newDescription = "Nullam tempor lacus nisl, eget tempus quam maximus malesuada.";

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
        var createdResult = EventAggregate.Create();
        var eventAggregate = createdResult.PayLoad;
        eventAggregate.EventStatus = EventStatus.Active;
        var newDescription = "Nullam tempor lacus nisl, eget tempus quam maximus malesuada.";

        // Act
        var result = eventAggregate.UpdateEventDescription(newDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EventAggregateErrors.ActiveEventCantBeModified, result.Errors.First());
    }
    
    //Validation for EventDescription
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

