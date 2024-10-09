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

public class SqliteApplicationFactory : WebApplicationFactory<Program>
{
    private IServiceCollection _serviceCollection;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DmContext>));
            services.RemoveAll(typeof(DbContextOptions<VeadatabaseProductionContext>));
            services.AddDbContext<DmContext>(options =>
            {
                options.UseInMemoryDatabase(GetConnectionString());
            });
            
            services.AddDbContext<VeadatabaseProductionContext>(options =>
            {
                options.UseSqlite(GetConnectionString());
            });
            
            services.AddScoped<TimeProvider, FakeTimeProvider>();
            SetupCleanDatabase(services);
        });
    }

    private string GetConnectionString()
    {
        var databasePath =
            "../../Infrastructure/ViaEventAssociation.Infrastructure.EfcDmPersistence/TestProduction.db";
        return $"Data Source={databasePath}";
    }

    protected override void Dispose(bool disposing)
    {
        DmContext dmContext = _serviceCollection.BuildServiceProvider().GetService<DmContext>()!;
        dmContext.Database.EnsureDeleted();
        base.Dispose(disposing);
    }
    
    private void SetupCleanDatabase(IServiceCollection services)
    {
        DmContext dmContext = services.BuildServiceProvider().GetService<DmContext>()!;
        dmContext.Database.EnsureDeleted();
        dmContext.Database.EnsureCreated();
    }
}