using RentARide.Domain.Entities;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.Data;

namespace RentARide.Infrastructure.Repositories;

public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
{
    public AmenityRepository(ApplicationDbContext context) : base(context)
    {
    }
}
