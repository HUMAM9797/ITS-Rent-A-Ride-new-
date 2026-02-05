using RentARide.Domain.Common;
using RentARide.Domain.Enums;

namespace RentARide.Domain.Entities;

public class Rental : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public RentalStatus Status { get; set; } = RentalStatus.Active;

    // Foreign Keys
    public Guid UserId { get; set; }
    public Guid VehicleId { get; set; }

    // Navigation Properties
    public virtual User User { get; set; } = null!;
    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual ICollection<RentalAmenity> RentalAmenities { get; set; } = new List<RentalAmenity>();
}
