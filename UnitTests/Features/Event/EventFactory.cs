using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Features.Event;

public class EventFactory
{
    private EventAggregate _eventAggregate;

    private EventFactory()
    {
        var id = EventId.Create();
        _eventAggregate = EventAggregate.Create(id);
    }
    
    public static EventFactory Init()
    {
        return new EventFactory();
    }
    
    public EventAggregate Build()
    {
        return _eventAggregate;
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
}