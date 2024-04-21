using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using Xunit;

namespace IntegrationTests.Repositories;

public class EventRepositoryTestsFixture : IDisposable
{
    public DbContext Context { get; private set; }

    public EventRepositoryTestsFixture()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DmContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "testDb");
        Context = new DmContext(optionsBuilder.Options);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}

[CollectionDefinition("Database Collection")]
public class DatabaseCollection : ICollectionFixture<EventRepositoryTestsFixture>
{
   
}

[Collection("Database Collection")]
public class EventRepositoryTests
{
    private readonly EventRepositoryTestsFixture _fixture;

    public EventRepositoryTests(EventRepositoryTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestMethod1()
    {
        // Use _fixture.Context for testing
    }

    [Fact]
    public void TestMethod2()
    {
        // Use _fixture.Context for testing
    }
}