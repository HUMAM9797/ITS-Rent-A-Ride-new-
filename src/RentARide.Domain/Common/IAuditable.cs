namespace RentARide.Domain.Common;

/// <summary>
/// Interface for entities that track audit information.
/// Provides timestamps for creation and updates.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Date and time when the entity was created
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when the entity was last updated
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}
