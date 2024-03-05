using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event.SetNumberOfGuests;

public class SetNumberOfGuestsUnitTests
{
    [Fact]
    public void GivenEvent_AndStatusIsDraft_WhenNumberOfGuestsIsSet_AndNumberIsLessThan50_ThenSetSelectedValue()
    {
        var eventAggregate = EventFactory.Init()
            .WithStatus(EventStatus.Draft)
            .Build();
        // eventAggregate.SetNumberOfGuest();
        Assert.Equal(EventVisibility.Public, eventAggregate.EventVisibility);
    }
}