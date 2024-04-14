using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.EventPersistence;

public class EventConfiguration : IEntityTypeConfiguration<EventAggregate>
{
    public void Configure(EntityTypeBuilder<EventAggregate> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(e => e.Id,
                dbValue => EventId.FromGuid(dbValue));
        
        builder.ComplexProperty<EventTitle>(
            "EventTitle",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.Title)
                    .HasColumnName("Title");
            }
        );
        
        builder.ComplexProperty<EventDescription>(
            "EventDescription",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.Description)
                    .HasColumnName("Description");
            }
        );
        
        builder.Property<EventVisibility>("Visibility")
            .HasConversion(
                visibility => visibility.ToString(), 
                value => (EventVisibility)Enum.Parse(typeof(EventVisibility), value)
            );

        builder.ComplexProperty<EventCapacity>(
            "EventCapacity",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.Capacity)
                    .HasColumnName("Capacity");
            }
        );
        
        builder.Property<EventStatus>("Status")
            .HasConversion(
                status => status.ToString(), 
                value => (EventStatus)Enum.Parse(typeof(EventStatus), value)
            );
        
        // builder.OwnsOne<EventTimeInterval>("TimeInterval", ownedNavigationBuilder =>
        // {
        //     ownedNavigationBuilder.Property(valueObject => valueObject.Start)
        //         .HasColumnName("Start");
        //
        //     ownedNavigationBuilder.Property(valueObject => valueObject.End)
        //         .HasColumnName("End");
        // });
        
        //List of guests id missing 
        
        builder
            .HasMany<Invitation>("Invitations")
            .WithOne()
            .HasForeignKey("EventId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}