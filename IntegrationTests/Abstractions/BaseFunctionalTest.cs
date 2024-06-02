using IntegrationTests.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using Xunit;

namespace IntegrationTests.Abstractions;

public class BaseFunctionalTest : IClassFixture<ViaWebApplicationFactory>
{
    protected HttpClient Client { get; init; }
    protected DmContext DmContext { get; init; }
    
    public BaseFunctionalTest(ViaWebApplicationFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
        DmContext = scope.ServiceProvider.GetRequiredService<DmContext>();
    }
}