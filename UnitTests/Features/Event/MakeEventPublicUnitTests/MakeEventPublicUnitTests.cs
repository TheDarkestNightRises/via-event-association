using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPublicUnitTests;

public class MakeEventPublicUnitTests
{
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenVisibilitySetToPublic_ThenVisibilityIsPublic_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Draft;
        result.PayLoad.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Draft, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsReady_WhenVisibilitySetToPublic_ThenVisibilityIsPublic_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Ready;
        result.PayLoad.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Ready, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsActive_WhenVisibilitySetToPublic_ThenVisibilityIsPublic_AndStatusUnchanged()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.EventStatus = EventStatus.Active;
        result.PayLoad.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, result.PayLoad.EventVisibility);
        Assert.Equal(EventStatus.Active, result.PayLoad.EventStatus);
    }
    
    [Fact]
    public void GivenEvent_AndStatusIsCancelled_WhenVisibilitySetToPublic_ThenFailureMessageIsProvided()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        var eventAggregate = result.PayLoad;
        eventAggregate.EventStatus = EventStatus.Cancelled;
        Assert.Equal(EventAggregateErrors.CantMakeCancelledEventPublic,eventAggregate.MakeEventPublic());
        Assert.NotEqual(EventVisibility.Public, result.PayLoad.EventVisibility);
    }
}