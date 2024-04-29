using Microsoft.EntityFrameworkCore;
using UnitTests.Features.Event;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcQueries;
using ViaEventAssociation.Infrastructure.EfcQueries.SeedFactory;

namespace IntegrationTests.Queries
{
    public class DataFixture : IDisposable
    {
        public VeadatabaseProductionContext Context { get; private set; }

        public DataFixture()
        {
            var options = new DbContextOptionsBuilder<VeadatabaseProductionContext>()
                .UseInMemoryDatabase(databaseName: "DataFixture")
                .Options;
            Context = new VeadatabaseProductionContext(options);

            Context.Seed();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}