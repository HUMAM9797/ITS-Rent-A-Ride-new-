using RentARide.Domain.Entities;
using RentARide.Domain.Enums;

namespace RentARide.Domain.Interfaces;

public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
    Task<IEnumerable<Vehicle>> GetByStatusAsync(VehicleStatus status);
}
