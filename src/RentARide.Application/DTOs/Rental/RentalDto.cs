using RentARide.Domain.Enums;

namespace RentARide.Application.DTOs.Rental;

public class RentalDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public RentalStatus Status { get; set; }
    public string VehicleModel { get; set; } = string.Empty;
    public string UserFullName { get; set; } = string.Empty;
}
