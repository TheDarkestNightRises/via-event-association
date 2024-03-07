using System.Security.AccessControl;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.MakeEventPrivate;

public class MakeEventPrivateUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MakeEventPrivateUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    // UC6.S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    // UC6.S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventStatus.Draft, eventAggregate.EventStatus);
    }
    
    // UC6.S1
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    // UC6.S1
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventStatus.Ready, eventAggregate.EventStatus);
    }
    
    // UC6.S2
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventIsPublic_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithVisibility(EventVisibility.Public)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    // UC6.S2
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventIsPublic_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .WithVisibility(EventVisibility.Public)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventStatus.Draft, eventAggregate.EventStatus);
    }
    
    // UC6.S2
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventIsPublic_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithVisibility(EventVisibility.Public)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    // UC6.S2
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventIsPublic_WhenVisibilitySetToPrivate_ThenAndStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .WithVisibility(EventVisibility.Public)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventStatus.Ready, eventAggregate.EventStatus);
    }
    
    // UC6.F1
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPrivate_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithVisibility(EventVisibility.Public)
            .Build();
        var result = eventAggregate.MakeEventPrivate();
        Assert.Equal(EventAggregateErrors.CantMakeActiveEventPrivate,result.Errors.First());
    }
    
    // UC6.F1
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithVisibility(EventVisibility.Public)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.NotEqual(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    // UC6.F2
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPrivate_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        var result = eventAggregate.MakeEventPrivate();
        Assert.Equal(EventAggregateErrors.CantMakeCancelledEventPrivate,result.Errors.First());
    }
    
    // UC6.F2
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
}