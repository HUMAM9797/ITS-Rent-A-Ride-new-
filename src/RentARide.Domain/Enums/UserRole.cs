namespace RentARide.Domain.Enums;

/// <summary>
/// Defines the roles available in the system
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Customer role - can browse and rent vehicles
    /// </summary>
    Customer = 0,

    /// <summary>
    /// Admin role - has full access to manage the system
    /// </summary>
    Admin = 1
}
