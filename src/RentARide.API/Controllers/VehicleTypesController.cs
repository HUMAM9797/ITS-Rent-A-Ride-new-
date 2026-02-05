using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicle;
using RentARide.Application.Services.Interfaces;

namespace RentARide.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleTypesController : ControllerBase
{
    private readonly IVehicleTypeService _vehicleTypeService;

    public VehicleTypesController(IVehicleTypeService vehicleTypeService)
    {
        _vehicleTypeService = vehicleTypeService;
    }

    /// <summary>
    /// Get all vehicle types (cached for 24 hours)
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<VehicleTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVehicleTypes()
    {
        var result = await _vehicleTypeService.GetAllVehicleTypesAsync();

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage ?? "Failed to retrieve vehicle types"));

        return Ok(ApiResponse<IEnumerable<VehicleTypeDto>>.Ok(result.Data!, "Vehicle types retrieved successfully"));
    }
}
