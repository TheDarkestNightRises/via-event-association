using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Repository;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.EventPersistence;

public class EventRepository(DbContext context) : RepositoryBase<EventAggregate,EventId>(context), IEventRepository
{
    
}