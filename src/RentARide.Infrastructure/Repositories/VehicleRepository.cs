using Microsoft.EntityFrameworkCore;
using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.Data;

namespace RentARide.Infrastructure.Repositories;

public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
    {
        return await _dbSet
            .Where(v => v.Status == VehicleStatus.Available && !v.IsDeleted)
            .Include(v => v.VehicleType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetByStatusAsync(VehicleStatus status)
    {
        return await _dbSet
            .Where(v => v.Status == status && !v.IsDeleted)
            .Include(v => v.VehicleType)
            .ToListAsync();
    }
}
