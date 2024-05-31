using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Time.Testing;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;

namespace IntegrationTests.Abstractions;

public class ViaWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DmContext>));
            services.RemoveAll(typeof(DbContextOptions<VeadatabaseProductionContext>));
            services.AddDbContext<DmContext>(options =>
            {
                options.UseInMemoryDatabase("Test");
            });
            
            services.AddDbContext<VeadatabaseProductionContext>(options =>
            {
                options.UseInMemoryDatabase("Test");
            });
            
            services.AddScoped<TimeProvider, FakeTimeProvider>();
        });
    }
}