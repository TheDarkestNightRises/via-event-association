using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;

public class DmContext(DbContextOptions<DmContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DmContext).Assembly);
    }

    public DbSet<EventAggregate> Events => Set<EventAggregate>();
    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<GuestAggregate> Guests => Set<GuestAggregate>();
}