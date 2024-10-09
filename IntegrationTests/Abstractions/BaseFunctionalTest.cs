using IntegrationTests.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Features.Event;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.SeedFactory;
using Xunit;

namespace IntegrationTests.Abstractions;

public class BaseFunctionalTest : IClassFixture<ViaWebApplicationFactory>
{
    protected HttpClient Client { get; init; }
    protected DmContext DmContext { get; init; }
    protected VeadatabaseProductionContext ReadContext { get; init; }

    public BaseFunctionalTest(ViaWebApplicationFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
        DmContext = scope.ServiceProvider.GetRequiredService<DmContext>();
        ReadContext = scope.ServiceProvider.GetRequiredService<VeadatabaseProductionContext>();
        SeedDmContext();
        SeedReadContext();
    }

    private void SeedDmContext()
    {
        DmContext.Database.EnsureDeleted();
        DmContext.Database.EnsureCreated();
        DmContext.Events.Add(EventFactory.ValidEvent());
        DmContext.Events.Add(EventFactory.ActiveEvent());
        DmContext.SaveChanges();
    }
    
    private void SeedReadContext()
    {
        ReadContext.Seed();
        ReadContext.SaveChanges();
    }
}