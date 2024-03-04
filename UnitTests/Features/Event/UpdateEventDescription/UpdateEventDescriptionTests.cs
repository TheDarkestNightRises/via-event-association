using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.UpdateEventDescription;

public class UpdateEventDescriptionTests
{
    //UC3.S1
    
    //UC3.S2
    //UC3.F1
    
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

