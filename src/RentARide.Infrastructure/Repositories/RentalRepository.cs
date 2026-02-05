using Microsoft.EntityFrameworkCore;
using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.Data;

namespace RentARide.Infrastructure.Repositories;

public class RentalRepository : GenericRepository<Rental>, IRentalRepository
{
    public RentalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Rental>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(r => r.UserId == userId && !r.IsDeleted)
            .Include(r => r.Vehicle)
            .Include(r => r.RentalAmenities)
                .ThenInclude(ra => ra.Amenity)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetByVehicleIdAsync(Guid vehicleId)
    {
        return await _dbSet
            .Where(r => r.VehicleId == vehicleId && !r.IsDeleted)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetActiveRentalsAsync()
    {
        return await _dbSet
            .Where(r => r.Status == RentalStatus.Active && !r.IsDeleted)
            .Include(r => r.User)
            .Include(r => r.Vehicle)
            .ToListAsync();
    }
}
