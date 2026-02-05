using RentARide.Domain.Common;

namespace RentARide.Domain.Entities;

public class RentalAmenity : BaseEntity
{
    // Foreign Keys
    public Guid RentalId { get; set; }
    public Guid AmenityId { get; set; }

    // Navigation Properties
    public virtual Rental Rental { get; set; } = null!;
    public virtual Amenity Amenity { get; set; } = null!;
}
