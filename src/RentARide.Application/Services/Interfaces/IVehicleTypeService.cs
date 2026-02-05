using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicle;

namespace RentARide.Application.Services.Interfaces;

public interface IVehicleTypeService
{
    Task<ServiceResult<IEnumerable<VehicleTypeDto>>> GetAllVehicleTypesAsync();
}
