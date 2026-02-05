using Mapster;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicle;
using RentARide.Application.Services.Interfaces;
using RentARide.Domain.Entities;
using RentARide.Domain.Interfaces;

namespace RentARide.Application.Services.Implementations;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<VehicleDto>> GetVehicleByIdAsync(Guid id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
        if (vehicle == null) return ServiceResult<VehicleDto>.Failure("Vehicle not found.");

        return ServiceResult<VehicleDto>.Success(vehicle.Adapt<VehicleDto>());
    }

    public async Task<ServiceResult<IEnumerable<VehicleDto>>> GetAllVehiclesAsync()
    {
        var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
        return ServiceResult<IEnumerable<VehicleDto>>.Success(vehicles.Adapt<IEnumerable<VehicleDto>>());
    }

    public async Task<ServiceResult<VehicleDto>> CreateVehicleAsync(CreateVehicleDto createVehicleDto)
    {
        var vehicle = createVehicleDto.Adapt<Vehicle>();
        await _unitOfWork.Vehicles.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<VehicleDto>.Success(vehicle.Adapt<VehicleDto>());
    }

    public async Task<ServiceResult<bool>> UpdateVehiclePriceAsync(Guid id, UpdateVehiclePriceDto updatePriceDto)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
        if (vehicle == null) return ServiceResult<bool>.Failure("Vehicle not found.");

        vehicle.DailyPrice = updatePriceDto.NewDailyPrice;
        await _unitOfWork.Vehicles.UpdateAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<bool>> DeleteVehicleAsync(Guid id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
        if (vehicle == null) return ServiceResult<bool>.Failure("Vehicle not found.");

        await _unitOfWork.Vehicles.DeleteAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }
}
