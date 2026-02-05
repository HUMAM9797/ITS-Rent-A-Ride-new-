namespace RentARide.Domain.Enums;

/// <summary>
/// Defines the current status of a vehicle
/// </summary>
public enum VehicleStatus
{
    /// <summary>
    /// Vehicle is available for rental
    /// </summary>
    Available = 0,

    /// <summary>
    /// Vehicle is currently rented out
    /// </summary>
    Rented = 1,

    /// <summary>
    /// Vehicle is under maintenance and not available
    /// </summary>
    Maintenance = 2
}
