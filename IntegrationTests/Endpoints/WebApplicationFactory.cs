using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Time.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;

namespace IntegrationTests.Endpoints;

public class ViaWebApplicationFactory : WebApplicationFactory<Program>
{
    private IServiceCollection _serviceCollection;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DmContext));
            services.RemoveAll(typeof(VeadatabaseProductionContext));
            services.RemoveAll<DmContext>();
            services.RemoveAll<VeadatabaseProductionContext>();

            string conn = GetConnectionString();
            services.AddDbContext<DmContext>(options => options.UseSqlite(conn));
            services.AddScoped<TimeProvider, FakeTimeProvider>();
            SetupCleanDatabase(services);
        });
    }

    private string GetConnectionString()
    {
        string testDbName = "Test" + Guid.NewGuid() + "db";
        return "Data source =" + testDbName;
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