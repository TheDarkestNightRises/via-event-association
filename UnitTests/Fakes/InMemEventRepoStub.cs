using System.Collections;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace UnitTests.Fakes;

public class InMemEventRepoStub : IEventRepository
{
    public List<EventAggregate> Events { get; set; } = [];
    
    public Task<EventAggregate> GetAsync(EventId eventId)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(EventAggregate createdEvent)
    {
        Events.Add(createdEvent);
        return Task.CompletedTask;
    }

}