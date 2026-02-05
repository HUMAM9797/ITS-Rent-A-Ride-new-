namespace RentARide.Application.DTOs.Vehicle;

public class CreateVehicleDto
{
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public Guid VehicleTypeId { get; set; }
}
