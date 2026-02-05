namespace RentARide.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IVehicleRepository Vehicles { get; }
    IRentalRepository Rentals { get; }
    IVehicleTypeRepository VehicleTypes { get; }
    IAmenityRepository Amenities { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
