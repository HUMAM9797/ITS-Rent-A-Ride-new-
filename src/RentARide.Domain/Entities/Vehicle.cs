using RentARide.Domain.Common;
using RentARide.Domain.Enums;

namespace RentARide.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.Available;

    // Foreign Keys
    public Guid VehicleTypeId { get; set; }

    // Navigation Properties
    public virtual VehicleType VehicleType { get; set; } = null!;
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    public virtual VehicleMaintenance? VehicleMaintenance { get; set; }
}
