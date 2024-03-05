using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPrivate;

public class MakeEventPrivateUnitTests
{
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Draft;
        result.PayLoad.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Draft, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventAlreadyPrivate_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Ready;
        result.PayLoad.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Ready, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsDraft_EventIsPublic_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Draft;
        result.PayLoad.EventVisibility = EventVisibility.Public;
        result.PayLoad.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Draft, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsReady_EventIsPublic_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Ready;
        result.PayLoad.EventVisibility = EventVisibility.Public;
        result.PayLoad.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Ready, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPrivate_ThenFailureMessageIsProvided()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        var eventAggregate = result.PayLoad;
        eventAggregate.EventStatus = EventStatus.Active;
        result.PayLoad.EventVisibility = EventVisibility.Public;
        Assert.Equal(EventAggregateErrors.CantMakeActiveEventPrivate,eventAggregate.MakeEventPrivate());
        Assert.NotEqual(EventVisibility.Private, result.PayLoad.EventVisibility);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPrivate_ThenFailureMessageIsProvided()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        var eventAggregate = result.PayLoad;
        eventAggregate.EventStatus = EventStatus.Cancelled;
        Assert.Equal(EventAggregateErrors.CantMakeCancelledEventPrivate,eventAggregate.MakeEventPrivate());
        Assert.NotEqual(EventVisibility.Private, result.PayLoad.EventVisibility);
    }
    
}