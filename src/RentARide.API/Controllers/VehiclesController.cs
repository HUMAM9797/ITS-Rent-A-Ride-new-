using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicle;
using RentARide.Application.Services.Interfaces;

namespace RentARide.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly IValidator<CreateVehicleDto> _createValidator;
    private readonly IValidator<UpdateVehiclePriceDto> _updatePriceValidator;

    public VehiclesController(
        IVehicleService vehicleService,
        IValidator<CreateVehicleDto> createValidator,
        IValidator<UpdateVehiclePriceDto> updatePriceValidator)
    {
        _vehicleService = vehicleService;
        _createValidator = createValidator;
        _updatePriceValidator = updatePriceValidator;
    }

    /// <summary>
    /// Get all available vehicles
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<VehicleDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVehicles()
    {
        var result = await _vehicleService.GetAllVehiclesAsync();

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage ?? "Failed to retrieve vehicles"));

        return Ok(ApiResponse<IEnumerable<VehicleDto>>.Ok(result.Data!, "Vehicles retrieved successfully"));
    }

    /// <summary>
    /// Get a specific vehicle by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<VehicleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVehicleById(Guid id)
    {
        var result = await _vehicleService.GetVehicleByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage ?? "Vehicle not found"));

        return Ok(ApiResponse<VehicleDto>.Ok(result.Data!, "Vehicle retrieved successfully"));
    }

    /// <summary>
    /// Create a new vehicle (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<VehicleDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
    {
        var validationResult = await _createValidator.ValidateAsync(createVehicleDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Validation failed", errors));
        }

        var result = await _vehicleService.CreateVehicleAsync(createVehicleDto);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage ?? "Failed to create vehicle"));

        return CreatedAtAction(
            nameof(GetVehicleById),
            new { id = result.Data!.Id },
            ApiResponse<VehicleDto>.Ok(result.Data!, "Vehicle created successfully"));
    }

    /// <summary>
    /// Update vehicle price (Admin only)
    /// </summary>
    [HttpPatch("{id:guid}/price")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateVehiclePrice(Guid id, [FromBody] UpdateVehiclePriceDto updatePriceDto)
    {
        var validationResult = await _updatePriceValidator.ValidateAsync(updatePriceDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Validation failed", errors));
        }

        var result = await _vehicleService.UpdateVehiclePriceAsync(id, updatePriceDto);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage ?? "Vehicle not found"));

        return Ok(ApiResponse<object>.Ok(null!, "Vehicle price updated successfully"));
    }

    /// <summary>
    /// Delete a vehicle (Admin only)
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        var result = await _vehicleService.DeleteVehicleAsync(id);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage ?? "Vehicle not found"));

        return Ok(ApiResponse<object>.Ok(null!, "Vehicle deleted successfully"));
    }
}
