using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.GuestPersistence;

public class GuestConfiguration : IEntityTypeConfiguration<GuestAggregate>
{
    public void Configure(EntityTypeBuilder<GuestAggregate> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(e => e.Id,
                dbValue => GuestId.FromGuid(dbValue));
        
        builder.ComplexProperty<GuestViaEmail>(
            "GuestViaEmail",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.ViaEmail)
                    .HasColumnName("Email");
            }
        );
        
        builder.ComplexProperty<GuestFirstName>(
            "GuestFirstName",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.FirstName)
                    .HasColumnName("FirstName");
            }
        );
        
        builder.ComplexProperty<GuestLastName>(
            "GuestLastName",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.LastName)
                    .HasColumnName("LastName");
            }
        );
        
        builder.ComplexProperty<GuestPictureUrl>(
            "GuestPictureUrl",
            propBuilder =>
            {
                propBuilder.Property(vo => vo.PictureUrl)
                    .HasColumnName("PictureUrl");
            }
        );
    }

}