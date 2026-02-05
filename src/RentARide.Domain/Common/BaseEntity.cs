namespace RentARide.Domain.Common;

/// <summary>
/// Base entity class that all domain entities inherit from.
/// Provides common properties like Id and timestamps.
/// </summary>
public abstract class BaseEntity : IAuditable, ISoftDeletable
{
    /// <summary>
    /// Unique identifier for the entity
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the entity was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the entity has been soft deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Date and time when the entity was soft deleted
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}
