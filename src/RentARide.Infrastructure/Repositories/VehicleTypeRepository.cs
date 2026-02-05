using RentARide.Domain.Entities;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.Data;

namespace RentARide.Infrastructure.Repositories;

public class VehicleTypeRepository : GenericRepository<VehicleType>, IVehicleTypeRepository
{
    public VehicleTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
