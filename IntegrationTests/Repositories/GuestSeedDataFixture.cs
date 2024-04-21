using Microsoft.EntityFrameworkCore;
using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using Xunit;

namespace IntegrationTests.Repositories;


public class GuestSeedDataFixture : IDisposable
{
    public DmContext Context { get; private set; } 
    
    public GuestSeedDataFixture()
    {
        var options = new DbContextOptionsBuilder<DmContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        Context = new DmContext(options);

        var guest = GuestFactory.ValidGuest();
        Context.Guests.Add(guest);
        Context.SaveChanges();
    }
    
    public void Dispose()
    {
        Context.Dispose();
    }
    
    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection : ICollectionFixture<EventRepositoryTestsFixture>
    {
   
    }
    
}