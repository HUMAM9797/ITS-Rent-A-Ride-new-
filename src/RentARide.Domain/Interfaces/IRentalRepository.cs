using RentARide.Domain.Entities;

namespace RentARide.Domain.Interfaces;

public interface IRentalRepository : IGenericRepository<Rental>
{
    Task<IEnumerable<Rental>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Rental>> GetByVehicleIdAsync(Guid vehicleId);
    Task<IEnumerable<Rental>> GetActiveRentalsAsync();
}
