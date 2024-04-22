using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ViaEventAssociation.Infrastructure.EfcQueries;

public partial class VeadatabaseProductionContext(DbContextOptions options)  : DbContext(options)
{
    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source = C:\\Github\\C#\\via-event-association\\src\\Infrastructure\\ViaEventAssociation.Infrastructure.EfcQueries\\VEADatabaseProduction.db");
        */

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
