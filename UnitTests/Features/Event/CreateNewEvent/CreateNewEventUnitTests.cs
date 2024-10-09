using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.CreateNewEvent;

public class CreateNewEventUnitTests
{
    //Event Id
    [Fact]
    public void GivenEventId_WhenCreated_ThenIdIsNotEmpty()
    {
        var result = EventId.Create();
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    //UC1.S1 
    [Fact]
    public void GivenEvent_WhenCreated_ThenStatusIsDraft()
    {
        var eventId = new EventId();
        var result = EventAggregate.Create(eventId);
        Assert.NotNull(result);
        Assert.Equal(EventStatus.Draft, result.PayLoad.EventStatus);
    }

    [Fact]
    public void GivenEvent_WhenCreated_ThenCapacityIsFive()
    {
        var eventId = new EventId();
        var result = EventAggregate.Create(eventId);
        Assert.NotNull(result.PayLoad);
        Assert.Equal(5 , (int) result.PayLoad.EventCapacity);
    }
    
    //UC1.S2
    [Fact]
    public void GivenEvent__WhenEventIsCreated_ThenTitleIsSetToWorkingTitle()
    {
        var eventId = new EventId();
        var result = EventAggregate.Create(eventId);
        Assert.NotNull(result.PayLoad);        
        Assert.Equal("Working Title", (string) result.PayLoad.EventTitle);
    }
    
    //UC1.S3
    [Fact]
    public void GivenEvent_WhenCreated_ThenDescriptionIsEmpty()
    {
        var eventId = new EventId();
        var result = EventAggregate.Create(eventId);
        var eventDescription = result.PayLoad.EventDescription;
        Assert.NotNull(eventDescription);
        Assert.True((string) eventDescription == "");
    }
    
    //UC1.S4
    [Fact]
    public void GivenEvent_WhenCreated_Then_VisibilityIsPrivate()
    {
        var eventId = new EventId();
        var result = EventAggregate.Create(eventId);
        Assert.NotNull(result.PayLoad);
        Assert.Equal(EventVisibility.Private, result.PayLoad.EventVisibility);
    }
}