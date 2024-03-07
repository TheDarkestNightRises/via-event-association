using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPublicUnitTests;

public class MakeEventPublicUnitTests
{
    // UC5.S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenVisibilitySetToPublic_ThenVisibilityIsPublic()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        eventAggregate.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, eventAggregate.EventVisibility);
    }
    
    // UC5.S1
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenVisibilitySetToPublic_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        eventAggregate.MakeEventPublic();
        Assert.Equal(EventStatus.Draft, eventAggregate.EventStatus);
    }
    
    // UC5.S1
    [Fact]
    public void GivenEvent_AndStatusIsReady_WhenVisibilitySetToPublic_ThenVisibilityIsPublic()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        eventAggregate.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, eventAggregate.EventVisibility);
    }
    
    // UC5.S1
    [Fact]
    public void GivenEvent_AndStatusIsReady_WhenVisibilitySetToPublic_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Ready)
            .Build();
        eventAggregate.MakeEventPublic();
        Assert.Equal(EventStatus.Ready, eventAggregate.EventStatus);
    }
    
    // UC5.S1
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPublic_ThenVisibilityIsPublic()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();
        eventAggregate.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, eventAggregate.EventVisibility);
    }
    
    // UC5.S1
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPublic_ThenStatusUnchanged()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Active)
            .Build();
        eventAggregate.MakeEventPublic();
        Assert.Equal(EventStatus.Active, eventAggregate.EventStatus);
    }
    
    // UC5.F1
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPublic_ThenFailureMessageIsProvided()
    {
         var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Cancelled)
            .Build();
         var result = eventAggregate.MakeEventPublic();
        Assert.Equal(EventAggregateErrors.CantMakeCancelledEventPublic,result.Errors.First());
    }
    
      
}