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
            .Build();
        var capacity = new EventCapacity(numberOfGuests);
        eventAggregate.SetNumberOfGuests(capacity);
        Assert.Equal(capacity, eventAggregate.EventCapacity);
    }
}