using RentARide.Domain.Enums;

namespace RentARide.Application.DTOs.Vehicle;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public VehicleStatus Status { get; set; }
    public string VehicleTypeName { get; set; } = string.Empty;
}
