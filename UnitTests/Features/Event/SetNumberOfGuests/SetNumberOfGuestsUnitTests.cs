﻿using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.SetNumberOfGuests;

public class SetNumberOfGuestsUnitTests
{
    // UC7.S1
    [Theory]
    [InlineData(50)]
    [InlineData(42)]
    [InlineData(36)]
    [InlineData(21)]
    [InlineData(5)]
    public void GivenEvent_AndStatusIsDraft_WhenNumberOfGuestsIsSet_AndNumberIsLessOrEqualTo50_ThenCapacityIsSetToSelectedValue(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithCapacity(EventCapacity.Create(5).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(capacity, eventAggregate.EventCapacity);
    }
    
    // UC7.S1
    [Theory]
    [InlineData(50)]
    [InlineData(42)]
    [InlineData(36)]
    [InlineData(21)]
    [InlineData(5)]
    public void GivenEvent_AndStatusIsReady_WhenNumberOfGuestsIsSet_AndNumberIsLessOrEqualTo50_ThenCapacityIsSetToSelectedValue(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithCapacity(EventCapacity.Create(5).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(capacity, eventAggregate.EventCapacity);
    }
    
    // UC7.S2
    [Theory]
    [InlineData(5)]
    [InlineData(21)]
    [InlineData(36)]
    [InlineData(42)]
    [InlineData(50)]
    public void GivenEvent_AndStatusIsDraft_WhenNumberOfGuestsIsSet_AndNumberIsBiggerOrEqualTo50_ThenCapacityIsSetToSelectedValue(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithCapacity(EventCapacity.Create(5).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(capacity, eventAggregate.EventCapacity);
    }
    
    // UC7.S2
    [Theory]
    [InlineData(5)]
    [InlineData(21)]
    [InlineData(36)]
    [InlineData(42)]
    [InlineData(50)]
    public void GivenEvent_AndStatusIsReady_WhenNumberOfGuestsIsSet_AndNumberIsBiggerOrEqualTo50_ThenCapacityIsSetToSelectedValue(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithCapacity(EventCapacity.Create(5).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(capacity, eventAggregate.EventCapacity);
    }
    
    // UC7.S3
    [Theory]
    [InlineData(5)]
    [InlineData(21)]
    [InlineData(36)]
    [InlineData(42)]
    [InlineData(50)]
    public void GivenEvent_AndStatusIsActive_WhenNumberOfGuestsIsSet_AndNumberIsBetween5And50_ThenCapacityIsSetToSelectedValue(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithCapacity(EventCapacity.Create(5).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(capacity, eventAggregate.EventCapacity);
    }
    
    // UC7.F1
    [Theory]
    [InlineData(39)]
    [InlineData(20)]
    [InlineData(5)]
    public void GivenEvent_AndStatusIsActive_WhenNumberOfGuestsIsReduced_ThenFailureMessageProvided(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithCapacity(EventCapacity.Create(40).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        var result = eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(EventAggregateErrors.NumberOfGuestsCanNotBeReduced,result.Errors.First());    
    }
    
    // UC7.F2
    [Theory]
    [InlineData(45)]
    [InlineData(42)]
    [InlineData(35)]
    [InlineData(9)]
    public void GivenEvent_AndStatusIsCancelled_WhenNumberOfGuestsIsChanged_ThenFailureMessageProvided(int numberOfGuests)
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .WithCapacity(EventCapacity.Create(40).PayLoad)
            .Build();
        var capacity = EventCapacity.Create(numberOfGuests).PayLoad;
        var result = eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(EventAggregateErrors.CancelledEventCantBeModified,result.Errors.First());    
    }
    // UC7.F3
    // UC7.F4
}