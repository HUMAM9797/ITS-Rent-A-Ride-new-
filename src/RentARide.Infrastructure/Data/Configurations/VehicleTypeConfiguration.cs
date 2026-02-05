using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentARide.Domain.Entities;

namespace RentARide.Infrastructure.Data.Configurations;

public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.HasKey(vt => vt.Id);

        builder.Property(vt => vt.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vt => vt.Description)
            .HasMaxLength(500);

        builder.HasMany(vt => vt.Vehicles)
            .WithOne(v => v.VehicleType)
            .HasForeignKey(v => v.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
