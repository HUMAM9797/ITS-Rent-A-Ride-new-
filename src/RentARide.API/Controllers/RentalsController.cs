using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Rental;
using RentARide.Application.Services.Interfaces;

namespace RentARide.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;
    private readonly IValidator<CreateRentalDto> _createValidator;

    public RentalsController(
        IRentalService rentalService,
        IValidator<CreateRentalDto> createValidator)
    {
        _rentalService = rentalService;
        _createValidator = createValidator;
    }

    /// <summary>
    /// Create a new rental
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<RentalDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateRental([FromBody] CreateRentalDto createRentalDto)
    {
        var validationResult = await _createValidator.ValidateAsync(createRentalDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Validation failed", errors));
        }

        var userId = GetCurrentUserId();
        if (userId == Guid.Empty)
            return Unauthorized(ApiResponse<object>.Fail("Invalid token"));

        var result = await _rentalService.CreateRentalAsync(userId, createRentalDto);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage ?? "Failed to create rental"));

        return CreatedAtAction(
            nameof(GetMyRentalHistory),
            ApiResponse<RentalDto>.Ok(result.Data!, "Rental created successfully"));
    }

    /// <summary>
    /// Get the current user's rental history
    /// </summary>
    [HttpGet("history")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RentalHistoryDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMyRentalHistory()
    {
        var userId = GetCurrentUserId();
        if (userId == Guid.Empty)
            return Unauthorized(ApiResponse<object>.Fail("Invalid token"));

        var result = await _rentalService.GetUserRentalHistoryAsync(userId);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage ?? "Failed to retrieve rental history"));

        return Ok(ApiResponse<IEnumerable<RentalHistoryDto>>.Ok(result.Data!, "Rental history retrieved successfully"));
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? User.FindFirst("sub")?.Value;

        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }
}
