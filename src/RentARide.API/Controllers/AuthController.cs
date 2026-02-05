using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Auth;
using RentARide.Application.Services.Interfaces;

namespace RentARide.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegisterDto> registerValidator,
        IValidator<LoginDto> loginValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    /// <summary>
    /// Register a new user account
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<TokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Validation failed", errors));
        }

        var result = await _authService.RegisterAsync(registerDto);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage ?? "Registration failed"));

        return Ok(ApiResponse<TokenResponseDto>.Ok(result.Data!, "Registration successful"));
    }

    /// <summary>
    /// Login with existing credentials
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<TokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var validationResult = await _loginValidator.ValidateAsync(loginDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Validation failed", errors));
        }

        var result = await _authService.LoginAsync(loginDto);

        if (!result.IsSuccess)
            return Unauthorized(ApiResponse<object>.Fail(result.ErrorMessage ?? "Invalid credentials"));

        return Ok(ApiResponse<TokenResponseDto>.Ok(result.Data!, "Login successful"));
    }
}
