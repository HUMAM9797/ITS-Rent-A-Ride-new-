namespace RentARide.Domain.Enums;

/// <summary>
/// Defines the current status of a rental
/// </summary>
public enum RentalStatus
{
    /// <summary>
    /// Rental is currently active
    /// </summary>
    Active = 0,

    /// <summary>
    /// Rental has been completed successfully
    /// </summary>
    Completed = 1,

    /// <summary>
    /// Rental has been cancelled
    /// </summary>
    Cancelled = 2
}
