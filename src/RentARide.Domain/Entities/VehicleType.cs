using RentARide.Domain.Common;

namespace RentARide.Domain.Entities;

public class VehicleType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation Properties
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
