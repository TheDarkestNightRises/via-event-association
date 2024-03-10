using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.MakeEventReady;

public class MakeEventReadyUnitTests
{
    private static TimeProvider? _timeProvider;

    public MakeEventReadyUnitTests()
    {
        // Set the current time in fake time provider in order to test methoids which make use of DateTime.Now
        _timeProvider = new FakeTimeProvider(new DateTime(2023,7,20,19,0,0));
    }
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithDefaultTitle, result.Errors.First());
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenNoTitle_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(new EventTitle(""))
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoTitle, result.Errors.First());
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenNoDescription_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoDescription, result.Errors.First());
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenVisibilityNone_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.None)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoVisibility, result.Errors.First());
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenCapacitySmallerThan5_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(new EventCapacity(4))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.EventCapacityCannotBeNegative, result.Errors.First());
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenCapacityBiggerThan50_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(new EventCapacity(51))
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.EventCapacityExceeded, result.Errors.First());
    }
    
    // UC8.F1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenTimeIntervalNotSet_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another title").PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(new EventCapacity(40))
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithNoTimeInterval, result.Errors.First());
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CancelledEventCanNotBeReadied, result.Errors.First());
    }
    
    // UC8.F3
    [Fact]
    public void GivenEvent_AndTimeIntervalIsPriorToReadyingTime_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithTitle(EventTitle.Create("Another Title").PayLoad)
            .WithTimeInterval(EventTimeInterval.Create(
                            new DateTime(2023,7,21,19,0,0), 
                            new DateTime(2023,7,21,21,0,0),
                            _timeProvider).PayLoad)
            .WithDescription(EventDescription.Create("Description").PayLoad)
            .WithVisibility(EventVisibility.Public)
            .WithCapacity(EventCapacity.Create(25).PayLoad)
            .Build();
        
        //set new current time after event has been created
        eventAggregate.EventTimeInterval!.CurrentTimeProvider = new FakeTimeProvider(new DateTime(2023, 7, 22, 10, 0, 0));
        //this simulates the following: 
        // event created: 20/07/2023
        // date of event: 21/07/2023 , but still draft
        // user tries to make event ready on 22.07 but event is in the past
        
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithTimeIntervalSetInThePast, result.Errors.First());
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
            .WithTimeInterval(EventTimeInterval.Create(
                new DateTime(2023,8,20,19,0,0), 
                new DateTime(2023,8,20,21,0,0),
                _timeProvider).PayLoad)
            .Build();
        var result = eventAggregate.MakeEventReady();
        Assert.Equal(EventAggregateErrors.CanNotReadyAnEventWithDefaultTitle, result.Errors.First());
    }
}