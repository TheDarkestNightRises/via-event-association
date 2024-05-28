using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using Xunit;

namespace IntegrationTests.Endpoints;

public class BaseIntegrationTest : IClassFixture<ViaWebApplicationFactory>
{
    private readonly IServiceScope _scope;
    private readonly DmContext _dbContext;
    
    public BaseIntegrationTest(ViaWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<DmContext>();
    }
    
}
