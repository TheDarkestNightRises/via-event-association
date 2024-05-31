using IntegrationTests.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using Xunit;

namespace IntegrationTests.Abstractions;

public class BaseIntegrationTest : IClassFixture<ViaWebApplicationFactory>
{
    private readonly IServiceScope _scope;
    protected readonly DmContext DbContext;
    
    public BaseIntegrationTest(ViaWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<DmContext>();
    }
}
