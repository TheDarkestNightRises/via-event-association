using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.MakeEventReady;

public class MakeEventReadyUnitTests
{
    // UC8.S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenVisibilitySetToPublic_ThenVisibilityIsPublic()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        eventAggregate.MakeEventReady();
        Assert.Equal(EventStatus.Ready, eventAggregate.EventStatus);
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenDefaultTitle_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Working Title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithDefaultTitle, result.Errors.First());
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenNoTitle_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(new EventTitle(""))
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoTitle, result.Errors.First());
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenNoDescription_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoDescription, result.Errors.First());
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenVisibilityNone_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.None)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoVisibility, result.Errors.First());
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenCapacitySmallerThan5_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(new EventCapacity(4))
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.EventCapacityCannotBeNegative, result.Errors.First());
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenCapacityBiggerThan50_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(new EventCapacity(51))
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.EventCapacityExceeded, result.Errors.First());
    }
    
    // UC8.F2
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .WithTitle(EventTitle.Create("Working Title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CancelledEventCanNotBeReadied, result.Errors.First());
    }
    
    // UC8.F4
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenTitleIsDefault_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Working Title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithDefaultTitle, result.Errors.First());
    }
}