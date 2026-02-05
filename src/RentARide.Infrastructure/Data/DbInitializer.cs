using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using RentARide.Domain.Interfaces;

namespace RentARide.Infrastructure.Data;

public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DbInitializer(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        // Ensure database is created
        await _context.Database.EnsureCreatedAsync();

        // Seed Admin User
        if (!_context.Users.Any(u => u.Email == "admin@rentaride.com"))
        {
            var admin = new User
            {
                FirstName = "System",
                LastName = "Admin",
                Email = "admin@rentaride.com",
                PasswordHash = _passwordHasher.HashPassword("Admin123!"),
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _context.Users.AddAsync(admin);
        }

        // Seed Vehicle Types
        if (!_context.VehicleTypes.Any())
        {
            var types = new List<VehicleType>
            {
                new() { Name = "Economy", Description = "Fuel-efficient compact cars", CreatedAt = DateTime.UtcNow },
                new() { Name = "SUV", Description = "Spacious sports utility vehicles", CreatedAt = DateTime.UtcNow },
                new() { Name = "Luxury", Description = "Premium driving experience", CreatedAt = DateTime.UtcNow }
            };
            await _context.VehicleTypes.AddRangeAsync(types);
            await _context.SaveChangesAsync(); // Save types first to get IDs if needed
        }

        // Seed Vehicles (if types exist)
        if (!_context.Vehicles.Any())
        {
            var suvType = _context.VehicleTypes.FirstOrDefault(t => t.Name == "SUV");
            if (suvType != null)
            {
                var vehicles = new List<Vehicle>
                {
                    new()
                    {
                        Model = "Toyota RAV4",
                        Year = 2024,
                        LicensePlate = "ABC-123",
                        DailyPrice = 50.0m,
                        Status = VehicleStatus.Available,
                        VehicleTypeId = suvType.Id,
                        CreatedAt = DateTime.UtcNow
                    },
                     new()
                    {
                        Model = "Honda CR-V",
                        Year = 2023,
                        LicensePlate = "XYZ-789",
                        DailyPrice = 48.0m,
                        Status = VehicleStatus.Available,
                        VehicleTypeId = suvType.Id,
                        CreatedAt = DateTime.UtcNow
                    }
                };
                await _context.Vehicles.AddRangeAsync(vehicles);
            }
        }

        // Seed Amenities
        if (!_context.Amenities.Any())
        {
            var amenities = new List<Amenity>
            {
                new() { Name = "GPS Navigation", Price = 10.0m, CreatedAt = DateTime.UtcNow },
                new() { Name = "Child Seat", Price = 15.0m, CreatedAt = DateTime.UtcNow },
                new() { Name = "WiFi Hotspot", Price = 8.0m, CreatedAt = DateTime.UtcNow }
            };
            await _context.Amenities.AddRangeAsync(amenities);
        }

        await _context.SaveChangesAsync();
    }
}
