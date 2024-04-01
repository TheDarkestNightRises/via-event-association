using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;

public interface IEventRepository
{
    Task<EventAggregate> GetAsync(EventId eventId);
}