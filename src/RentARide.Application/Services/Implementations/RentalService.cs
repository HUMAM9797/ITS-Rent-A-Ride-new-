using Mapster;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Rental;
using RentARide.Application.Services.Interfaces;
using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using RentARide.Domain.Interfaces;

namespace RentARide.Application.Services.Implementations;

public class RentalService : IRentalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicHolidayService _publicHolidayService;

    public RentalService(IUnitOfWork unitOfWork, IPublicHolidayService publicHolidayService)
    {
        _unitOfWork = unitOfWork;
        _publicHolidayService = publicHolidayService;
    }

    public async Task<ServiceResult<RentalDto>> CreateRentalAsync(Guid userId, CreateRentalDto createRentalDto)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(createRentalDto.VehicleId);
        if (vehicle == null) return ServiceResult<RentalDto>.Failure("Vehicle not found.");
        if (vehicle.Status != VehicleStatus.Available) return ServiceResult<RentalDto>.Failure("Vehicle is not available.");

        // Check if rental starts on a holiday
        bool isHoliday = await _publicHolidayService.IsPublicHolidayAsync(createRentalDto.StartDate);

        // Calculate Price
        int days = (createRentalDto.EndDate.Date - createRentalDto.StartDate.Date).Days;
        if (days <= 0) days = 1;

        decimal basePrice = days * vehicle.DailyPrice;
        if (isHoliday)
        {
            basePrice *= 1.1m; // 10% surcharge
        }

        var rental = new Rental
        {
            UserId = userId,
            VehicleId = createRentalDto.VehicleId,
            StartDate = createRentalDto.StartDate,
            EndDate = createRentalDto.EndDate,
            TotalPrice = basePrice,
            Status = RentalStatus.Active
        };

        // Add Amenities
        if (createRentalDto.AmenityIds != null && createRentalDto.AmenityIds.Any())
        {
            foreach (var amenityId in createRentalDto.AmenityIds)
            {
                var amenity = await _unitOfWork.Amenities.GetByIdAsync(amenityId);
                if (amenity != null)
                {
                    rental.RentalAmenities.Add(new RentalAmenity
                    {
                        AmenityId = amenityId
                    });
                    rental.TotalPrice += amenity.Price; // For simplicity, one-time fee
                }
            }
        }

        await _unitOfWork.Rentals.AddAsync(rental);

        // Update vehicle status
        vehicle.Status = VehicleStatus.Rented;
        await _unitOfWork.Vehicles.UpdateAsync(vehicle);

        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<RentalDto>.Success(rental.Adapt<RentalDto>());
    }

    public async Task<ServiceResult<IEnumerable<RentalHistoryDto>>> GetUserRentalHistoryAsync(Guid userId)
    {
        var rentals = await _unitOfWork.Rentals.GetByUserIdAsync(userId);
        return ServiceResult<IEnumerable<RentalHistoryDto>>.Success(rentals.Adapt<IEnumerable<RentalHistoryDto>>());
    }
}
