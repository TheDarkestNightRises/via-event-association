using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPublicUnitTests;

public class MakeEventPublicUnitTests
{
    [Fact]
    public void GivenEvent_WhenVisibilitySetToPublic_ThenVisibilityIsPublic()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.MakeEventPublic();
        Assert.Equal(EventVisibility.Public, result.PayLoad.EventVisibility);
    }
}