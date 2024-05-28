using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Context;

public partial class VeadatabaseProductionContext(DbContextOptions<VeadatabaseProductionContext> options)  : DbContext(options)
{
    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasIndex(e => e.EventId, "IX_Invitations_EventId");

            entity.HasIndex(e => e.GuestId, "IX_Invitations_GuestId");

            entity.HasOne(d => d.Event).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Guest).WithMany(p => p.Invitations).HasForeignKey(d => d.GuestId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
