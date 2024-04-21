using Microsoft.EntityFrameworkCore;
using UnitTests.Features.Event;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using Xunit;

namespace IntegrationTests.Repositories;

public class EventSeedDataFixture : IDisposable
{
    public DmContext Context { get; private set; } 
    
    public EventSeedDataFixture()
    {
        var options = new DbContextOptionsBuilder<DmContext>()
            .UseInMemoryDatabase(databaseName: "EventTestDatabase")
            .Options;
        Context = new DmContext(options);

        var validEvent = EventFactory.ValidEvent();
        Context.Events.Add(validEvent);
        Context.SaveChanges();
    }
    
    public void Dispose()
    {
        Context.Dispose();
    }
    
    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection : ICollectionFixture<EventSeedDataFixture>
    {
   
    }
}