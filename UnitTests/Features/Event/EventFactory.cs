using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event;

public class EventFactory
{
    private EventAggregate _eventAggregate;

    private EventFactory()
    {
        _eventAggregate = EventAggregate.Create();
    }
    
    public static EventFactory Init()
    {
        return new EventFactory();
    }
    
    public EventAggregate Build()
    {
        return _eventAggregate;
    }
    
    public EventFactory WithId(EventId id)
    {
        _eventAggregate.Id = id;
        return this;
    }
    
    public EventFactory WithTitle(EventTitle title)
    {
        _eventAggregate.EventTitle = title;
        return this;
    }
    
    public EventFactory WithDescription(EventDescription description)
    {
        _eventAggregate.EventDescription = description;
        return this;
    }

    public EventFactory WithVisibility(EventVisibility visibility)
    {
        _eventAggregate.EventVisibility = visibility;
        return this;
    }
    
    public EventFactory WithCapacity(EventCapacity capacity)
    {
        _eventAggregate.EventCapacity = capacity;
        return this;
    }
    
    public EventFactory WithStatus(EventStatus status)
    {
        _eventAggregate.EventStatus = status;
        return this;
    }
    public EventFactory WithTimeInterval(EventTimeInterval interval)
    {
        _eventAggregate.EventTimeInterval = interval;
        return this;
    }

    public static EventAggregate ValidEvent()
    {
        return Init()
            .WithId(new EventId(new Guid("0f8fad5b-d9cb-469f-a165-70867728950e")))
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithCapacity(new EventCapacity(10))
            .Build();
    }
    public static EventAggregate RandomIdValidEvent()
    {
        return Init()
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithCapacity(new EventCapacity(10))
            .Build();
    }
    
    public static EventAggregate PrivateEvent()
    {
        return Init()
            .WithId(new EventId(new Guid("f40879a0-4b77-4c8e-920e-4ac2e5580cb6")))
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .Build();
    }
    
    public static EventAggregate ActiveEvent()
    {
        return Init()
            .WithId(new EventId(new Guid("2dec42aa-ed8e-491d-9cf1-8df9506707cb")))
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithVisibility(EventVisibility.Private)
            .WithCapacity(new EventCapacity(10))
            .Build();
    }
    
    
    public static EventAggregate CanceledEvent()
    {
        return Init()
            .WithId(new EventId(new Guid("f40879a0-4b77-4c8e-920e-4ac2e5580cb6")))
            .WithTitle(new EventTitle("Title"))
            .WithDescription(new EventDescription("Description"))
            .WithCapacity(new EventCapacity(10))
            .WithStatus(EventStatus.Cancelled)
            .Build();
    }
}