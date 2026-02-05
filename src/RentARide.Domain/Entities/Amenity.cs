using RentARide.Domain.Common;

namespace RentARide.Domain.Entities;

public class Amenity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // Navigation Properties
    public virtual ICollection<RentalAmenity> RentalAmenities { get; set; } = new List<RentalAmenity>();
}
