using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaEventAssociation.Core.Domain.Aggregates.Entity.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.EventPersistence;

public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder
            .Property(i => i.Id)
            .HasConversion(e => e.Id,
                dbValue => InvitationId.FromGuid(dbValue));
        
        builder.Property<InvitationStatus>("Status")
            .HasConversion(
                status => status.ToString(), 
                value => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), value)
            );
        
        builder.Property(i => i.GuestId)
            .HasConversion(
            yId => yId.Id,
            dbValue => GuestId.FromGuid(dbValue)
        );

        builder.HasOne<GuestAggregate>()
            .WithMany()
            .HasForeignKey("GuestId");
    }
}