using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentARide.Domain.Entities;

namespace RentARide.Infrastructure.Data.Configurations;

public class VehicleMaintenanceConfiguration : IEntityTypeConfiguration<VehicleMaintenance>
{
    public void Configure(EntityTypeBuilder<VehicleMaintenance> builder)
    {
        builder.HasKey(vm => vm.Id);

        builder.Property(vm => vm.Description)
            .HasMaxLength(1000);

        builder.HasOne(vm => vm.Vehicle)
            .WithOne(v => v.VehicleMaintenance)
            .HasForeignKey<VehicleMaintenance>(vm => vm.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
