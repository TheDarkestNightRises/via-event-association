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

    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventStatus.Draft, eventAggregate.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.Equal(EventStatus.Ready, eventAggregate.EventStatus);
    }
    
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
    
    
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPrivate_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .WithVisibility(EventVisibility.Public)
            .Build();
        Assert.Equal(EventAggregateErrors.CantMakeActiveEventPrivate,eventAggregate.MakeEventPrivate());
    }
    
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
    
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPrivate_ThenFailureMessageIsProvided()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        Assert.Equal(EventAggregateErrors.CantMakeCancelledEventPrivate,eventAggregate.MakeEventPrivate());
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPrivate_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        eventAggregate.MakeEventPrivate();
        Assert.NotEqual(EventVisibility.Private, eventAggregate.EventVisibility);
    }
    
}