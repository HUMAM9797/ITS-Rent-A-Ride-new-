# 🚗 Rent-A-Ride API

A comprehensive car rental management system built with **ASP.NET Core** following **Clean Architecture** principles.

## 🏗️ Architecture

This project follows Clean Architecture with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                      RentARide.API                          │
│                  (Presentation Layer)                       │
│              Controllers, Middleware, Filters               │
└────────────────────┬────────────────────────────────────────┘
                     │
┌────────────────────┴────────────────────────────────────────┐
│                RentARide.Infrastructure                     │
│                 (Infrastructure Layer)                      │
│         EF Core, Repositories, External Services            │
└────────────────────┬────────────────────────────────────────┘
                     │
┌────────────────────┴────────────────────────────────────────┐
│                 RentARide.Application                       │
│                  (Application Layer)                        │
│          Use Cases, DTOs, Validators, Interfaces            │
└────────────────────┬────────────────────────────────────────┘
                     │
┌────────────────────┴────────────────────────────────────────┐
│                   RentARide.Domain                          │
│                    (Domain Layer)                           │
│              Entities, Enums, Domain Logic                  │
└─────────────────────────────────────────────────────────────┘
```

## 📦 Technology Stack

### Core Framework
- **.NET 8.0** - Latest LTS version
- **ASP.NET Core Web API** - RESTful API framework

### Database
- **PostgreSQL** - Primary database
- **Entity Framework Core 9.0** - ORM
- **Npgsql** - PostgreSQL provider

### Authentication & Security
- **JWT Bearer Authentication** - Token-based auth
- **BCrypt.Net** - Password hashing

### Validation & Mapping
- **FluentValidation** - Input validation
- **Mapster** - Object-to-object mapping

### Background Jobs
- **Hangfire** - Background job processing
- **Hangfire.PostgreSql** - PostgreSQL storage for Hangfire

### API Documentation
- **Swagger/OpenAPI** - API documentation

### Caching
- **IMemoryCache** - In-memory caching

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- PostgreSQL 12 or later
- Visual Studio 2022 / VS Code / Rider

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ITS-Rent-A-Ride-new-
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Update connection string**
   Edit `src/RentARide.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=rentaride;Username=postgres;Password=yourpassword"
     }
   }
   ```

5. **Run migrations** (after DbContext is created)
   ```bash
   cd src/RentARide.Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../RentARide.API
   dotnet ef database update --startup-project ../RentARide.API
   ```

6. **Run the API**
   ```bash
   cd src/RentARide.API
   dotnet run
   ```

7. **Access Swagger UI**
   Open browser: `https://localhost:5001/swagger`

## 📁 Project Structure

```
RentARide/
├── src/
│   ├── RentARide.Domain/
│   │   ├── Entities/          # Domain entities
│   │   ├── Enums/             # Enumerations
│   │   └── Interfaces/        # Repository interfaces
│   │
│   ├── RentARide.Application/
│   │   ├── DTOs/              # Data Transfer Objects
│   │   ├── Interfaces/        # Service interfaces
│   │   ├── Services/          # Business logic
│   │   ├── Validators/        # FluentValidation validators
│   │   └── Mappings/          # Mapster configurations
│   │
│   ├── RentARide.Infrastructure/
│   │   ├── Data/              # DbContext, Configurations
│   │   ├── Repositories/      # Repository implementations
│   │   ├── Services/          # External service implementations
│   │   └── BackgroundJobs/    # Hangfire jobs
│   │
│   └── RentARide.API/
│       ├── Controllers/       # API endpoints
│       ├── Middleware/        # Custom middleware
│       ├── Filters/           # Action filters
│       └── Extensions/        # Service registration
│
├── RentARide.sln
└── README.md
```

## 🔑 Key Features (Planned)

- ✅ User authentication and authorization
- ✅ Car inventory management
- ✅ Booking system
- ✅ Email notifications
- ✅ Background job processing
- ✅ Caching for performance
- ✅ Input validation
- ✅ API documentation

## 📚 API Endpoints (To Be Implemented)

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `POST /api/auth/refresh` - Refresh token

### Cars
- `GET /api/cars` - Get all available cars
- `GET /api/cars/{id}` - Get car by ID
- `POST /api/cars` - Add new car (Admin)
- `PUT /api/cars/{id}` - Update car (Admin)
- `DELETE /api/cars/{id}` - Delete car (Admin)

### Bookings
- `GET /api/bookings` - Get user bookings
- `GET /api/bookings/{id}` - Get booking by ID
- `POST /api/bookings` - Create new booking
- `PUT /api/bookings/{id}/cancel` - Cancel booking
- `GET /api/admin/bookings` - Get all bookings (Admin)

## 🛠️ Development Commands

```bash
# Build solution
dotnet build

# Run tests (when implemented)
dotnet test

# Run API
dotnet run --project src/RentARide.API

# Create migration
dotnet ef migrations add <MigrationName> --project src/RentARide.Infrastructure --startup-project src/RentARide.API

# Update database
dotnet ef database update --project src/RentARide.Infrastructure --startup-project src/RentARide.API

# Remove last migration
dotnet ef migrations remove --project src/RentARide.Infrastructure --startup-project src/RentARide.API
```

## 📝 Development Status

- ✅ **Phase 1**: Project Setup - COMPLETED
- ⏳ **Phase 2**: Domain Layer Implementation - PENDING
- ⏳ **Phase 3**: Application Layer Implementation - PENDING
- ⏳ **Phase 4**: Infrastructure Layer Implementation - PENDING
- ⏳ **Phase 5**: API Layer Implementation - PENDING
- ⏳ **Phase 6**: Testing - PENDING

## 🤝 Contributing

1. Create a feature branch
2. Make your changes
3. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 👥 Authors

- Your Name

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- ASP.NET Core Documentation
- Entity Framework Core Documentation
