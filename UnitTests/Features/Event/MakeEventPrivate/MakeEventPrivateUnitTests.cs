﻿using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPrivate;

public class MakeEventPrivateUnitTests
{
    [Fact]
    public void GivenEvent_WhenVisibilitySetToPrivate_ThenVisibilityIsPrivate()
    {
        Result<EventAggregate> result = EventAggregate.Create();
        Assert.NotNull(result.PayLoad);
        result.PayLoad.MakeEventPrivate();
        Assert.Equal(EventVisibility.Private, result.PayLoad.EventVisibility);
    }
}