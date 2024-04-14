using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;

public class DmContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DmContext).Assembly);
    }

    public DbSet<EventAggregate> Events => Set<EventAggregate>();
    // public DbSet<GuestAggregate> Guests => Set<GuestAggregate>();   
}