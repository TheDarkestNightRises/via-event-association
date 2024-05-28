using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Context;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<VeadatabaseProductionContext>
{
    public VeadatabaseProductionContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VeadatabaseProductionContext>();
        optionsBuilder.UseSqlite(@"Data Source = VEADatabaseProduction.db");
        return new VeadatabaseProductionContext(optionsBuilder.Options);
    }
}
