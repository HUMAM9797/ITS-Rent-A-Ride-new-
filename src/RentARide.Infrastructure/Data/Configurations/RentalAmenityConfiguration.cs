using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentARide.Domain.Entities;

namespace RentARide.Infrastructure.Data.Configurations;

public class RentalAmenityConfiguration : IEntityTypeConfiguration<RentalAmenity>
{
    public void Configure(EntityTypeBuilder<RentalAmenity> builder)
    {
        builder.HasKey(ra => ra.Id);

        builder.HasIndex(ra => new { ra.RentalId, ra.AmenityId })
            .IsUnique();

        builder.HasOne(ra => ra.Rental)
            .WithMany(r => r.RentalAmenities)
            .HasForeignKey(ra => ra.RentalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ra => ra.Amenity)
            .WithMany(a => a.RentalAmenities)
            .HasForeignKey(ra => ra.AmenityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
