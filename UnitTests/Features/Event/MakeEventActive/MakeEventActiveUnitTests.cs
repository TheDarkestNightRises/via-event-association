using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventActive;

public class MakeEventActiveUnitTests
{
    // S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenStatusSetToActive_ThenStatusIsReady()
    {
        var eventAggregate = EventFactory.Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .Build();
        eventAggregate.MakeEventActive();
        Assert.Equal(EventStatus.Active, eventAggregate.EventStatus);
    }
    
    //S1
    [Fact]
    public void GivenEvent_AndStatusIsSetToValidValues_WhenStatusSetToActive_ThenStatusIsSuccessful()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .Build();
        eventAggregate.MakeEventActive();
        Assert.Equal(EventStatus.Active, eventAggregate.EventStatus);
    }
    
    // S2
    [Fact]
    public void GivenEvent_AndStatusIsReady_WhenCreatorActivatesEvent_ThenStatusIsActive()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();
        eventAggregate.MakeEventActive();
        Assert.Equal(EventStatus.Active, eventAggregate.EventStatus);
    }
    
    // S3
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenCreatorActivatesEvent_ThenStatusRemainsActive()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();
        eventAggregate.MakeEventActive();
        Assert.Equal(EventStatus.Active, eventAggregate.EventStatus);
    }
    
    
    // F1
    [Fact]
    public void GivenEvent_AndStatusIsDraftWithNoValidValues_WhenCreatorActivatesEvent_ThenFailureMessageIsGiven()
    {
        var eventAggregate = EventFactory.Init()
            .WithTitle(new EventTitle(default))
            .WithDescription(new EventDescription(default))
            .WithVisibility(EventVisibility.None)
            .WithCapacity(new EventCapacity(2))
            .Build();
        var result = eventAggregate.MakeEventActive();
        Assert.Equal(EventAggregateErrors.InvalidEventData, result.Errors.First());
    }
    
    // F2
    [Fact]
    public void GivenEvent_AndEventStatusIsCancelled_WhenCreatorActivatesEvent_ThenFailureMessageIsGivenACancelledEventCannotBeActivated()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        var result = eventAggregate.MakeEventActive();
        Assert.Equal(EventAggregateErrors.CancelledEventCantBeActivated, result.Errors.First());
    }
}