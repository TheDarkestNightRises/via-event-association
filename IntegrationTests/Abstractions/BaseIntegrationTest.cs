using IntegrationTests.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.SeedFactory;
using Xunit;

namespace IntegrationTests.Abstractions;

public class BaseIntegrationTest : IClassFixture<ViaWebApplicationFactory>
{
    protected readonly DmContext DbContext;
    protected VeadatabaseProductionContext ReadContext { get; init; }

    
    public BaseIntegrationTest(ViaWebApplicationFactory factory)
    {
        var scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<DmContext>();
        ReadContext = scope.ServiceProvider.GetRequiredService<VeadatabaseProductionContext>();
        ReadContext.Seed();
    }
}
