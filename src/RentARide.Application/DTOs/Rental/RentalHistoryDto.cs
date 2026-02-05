namespace RentARide.Application.DTOs.Rental;

public class RentalHistoryDto
{
    public Guid RentalId { get; set; }
    public string VehicleModel { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}
