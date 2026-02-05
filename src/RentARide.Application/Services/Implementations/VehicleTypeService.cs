using Mapster;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicle;
using RentARide.Application.Services.Interfaces;
using RentARide.Domain.Interfaces;

namespace RentARide.Application.Services.Implementations;

public class VehicleTypeService : IVehicleTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private const string CacheKey = "VehicleTypes";

    public VehicleTypeService(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<ServiceResult<IEnumerable<VehicleTypeDto>>> GetAllVehicleTypesAsync()
    {
        var cachedTypes = _cacheService.Get<IEnumerable<VehicleTypeDto>>(CacheKey);
        if (cachedTypes != null)
        {
            return ServiceResult<IEnumerable<VehicleTypeDto>>.Success(cachedTypes);
        }

        var types = await _unitOfWork.VehicleTypes.GetAllAsync();
        var dtos = types.Adapt<IEnumerable<VehicleTypeDto>>();

        _cacheService.Set(CacheKey, dtos, TimeSpan.FromHours(24));

        return ServiceResult<IEnumerable<VehicleTypeDto>>.Success(dtos);
    }
}
