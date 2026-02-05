using Microsoft.EntityFrameworkCore;
using RentARide.Domain.Entities;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.Data;

namespace RentARide.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }
}
