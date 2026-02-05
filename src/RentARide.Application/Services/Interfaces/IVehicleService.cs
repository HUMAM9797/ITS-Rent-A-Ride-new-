using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicle;

namespace RentARide.Application.Services.Interfaces;

public interface IVehicleService
{
    Task<ServiceResult<VehicleDto>> GetVehicleByIdAsync(Guid id);
    Task<ServiceResult<IEnumerable<VehicleDto>>> GetAllVehiclesAsync();
    Task<ServiceResult<VehicleDto>> CreateVehicleAsync(CreateVehicleDto createVehicleDto);
    Task<ServiceResult<bool>> UpdateVehiclePriceAsync(Guid id, UpdateVehiclePriceDto updatePriceDto);
    Task<ServiceResult<bool>> DeleteVehicleAsync(Guid id);
}
