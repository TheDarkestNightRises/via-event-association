using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventActive;

public class MakeEventActiveUnitTests
{
    private static TimeProvider? _timeProvider;

    public MakeEventActiveUnitTests()
    {
        // Set the current time in fake time provider in order to test methoids which make use of DateTime.Now
        _timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
    }
    // S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenStatusSetToActive_ThenStatusIsReady()
    {
        var eventAggregate = EventFactory.Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        eventAggregate.MakeEventActive();
        Assert.Equal(EventStatus.Active, eventAggregate.EventStatus);
    }
    
    // S2
    [Fact]
    public void GivenEvent_AndStatusIsReady_WhenCreatorActivatesEvent_ThenStatusIsActive()
    {
        var eventAggregate = EventFactory.Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .WithStatus(EventStatus.Ready)
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
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
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
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .WithStatus(EventStatus.Draft)
            .Build();
        eventAggregate.EventTimeInterval!.CurrentTimeProvider =
            new FakeTimeProvider(new DateTime(2023, 9, 20, 10, 0, 0));
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