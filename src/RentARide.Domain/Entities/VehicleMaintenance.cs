using RentARide.Domain.Common;

namespace RentARide.Domain.Entities;

public class VehicleMaintenance : BaseEntity
{
    public string? Description { get; set; }
    public DateTime? LastMaintenanceDate { get; set; }
    public DateTime? NextMaintenanceDue { get; set; }

    // Foreign Keys
    public Guid VehicleId { get; set; }

    // Navigation Properties
    public virtual Vehicle Vehicle { get; set; } = null!;
}
