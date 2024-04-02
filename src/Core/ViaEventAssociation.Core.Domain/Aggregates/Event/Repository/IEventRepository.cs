using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;

public interface IEventRepository
{
    Task<EventAggregate> GetAsync(EventId eventId);
    Task AddAsync(EventAggregate createdEvent);
}