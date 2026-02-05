using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Common.Models;
using RentARide.Domain.Entities;
using RentARide.Domain.Interfaces;

namespace RentARide.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AmenitiesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AmenitiesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all available amenities
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Amenity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAmenities()
    {
        var amenities = await _unitOfWork.Amenities.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Amenity>>.Ok(amenities, "Amenities retrieved successfully"));
    }

    /// <summary>
    /// Create a new amenity (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<Amenity>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateAmenity([FromBody] Amenity amenity)
    {
        await _unitOfWork.Amenities.AddAsync(amenity);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAllAmenities), ApiResponse<Amenity>.Ok(amenity, "Amenity created successfully"));
    }
}
