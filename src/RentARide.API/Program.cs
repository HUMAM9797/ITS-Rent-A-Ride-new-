using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentARide.Application.BackgroundJobs;
using RentARide.Application.Services.Implementations;
using RentARide.Application.Services.Interfaces;
using RentARide.Application.Validators;
using RentARide.Application.Validators.Auth;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.Data;
using RentARide.Infrastructure.Data.Interceptors;
using RentARide.Infrastructure.ExternalAPIs;
using RentARide.Infrastructure.Repositories;
using RentARide.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext with PostgreSQL
// Configure DbContext with InMemory Database for testing
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<AuditLogInterceptor>();
    // options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    //        .AddInterceptors(interceptor);
    options.UseInMemoryDatabase("RentARideDb")
           .AddInterceptors(interceptor);
});

// Register Interceptors
builder.Services.AddSingleton<AuditLogInterceptor>();

// Register Repositories via UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();

// Register Infrastructure Services
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPublicHolidayService, PublicHolidayService>();
builder.Services.AddScoped<DbInitializer>();

// Register Application Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();

// Register External API Clients
builder.Services.AddHttpClient<NagerDateApiClient>();

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

// Add Memory Cache
builder.Services.AddMemoryCache();

// Register Background Services
builder.Services.AddHostedService<OverdueRentalJob>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "YourSuperSecretKeyWithAtLeast32Characters!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Configure Controllers
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Swagger/OpenAPI with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rent-A-Ride API",
        Version = "v1",
        Description = "A comprehensive car rental management API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rent-A-Ride API v1");
        options.RoutePrefix = string.Empty;
    });
}
else
{
    // Use exception handler in production/staging if not using developer exception page
    // app.UseExceptionHandler("/Error");
}

app.UseMiddleware<RentARide.API.Middleware.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbIntializer = services.GetRequiredService<DbInitializer>();
        // We use GetAwaiter().GetResult() synchronously here or await if top-level allows
        // Since we are in Program.cs with top-level statements, we can await
        await dbIntializer.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
