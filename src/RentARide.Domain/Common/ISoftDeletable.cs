namespace RentARide.Domain.Common;

/// <summary>
/// Interface for entities that support soft deletion.
/// Soft delete marks records as deleted without physically removing them from the database.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Indicates whether the entity has been soft deleted
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Date and time when the entity was soft deleted
    /// </summary>
    DateTime? DeletedAt { get; set; }
}
