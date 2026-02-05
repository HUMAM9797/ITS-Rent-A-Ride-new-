using RentARide.Domain.Common;
using RentARide.Domain.Enums;

namespace RentARide.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Customer;

    // Navigation Properties
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
