using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Auth;
using RentARide.Application.Services.Interfaces;
using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using RentARide.Domain.Interfaces;

namespace RentARide.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<ServiceResult<TokenResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return ServiceResult<TokenResponseDto>.Failure("User with this email already exists.");

        var user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PasswordHash = _passwordHasher.HashPassword(registerDto.Password),
            Role = UserRole.Customer
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var token = _tokenService.GenerateJwtToken(user);
        return ServiceResult<TokenResponseDto>.Success(new TokenResponseDto { Token = token, Email = user.Email });
    }

    public async Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);
        if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            return ServiceResult<TokenResponseDto>.Failure("Invalid email or password.");

        var token = _tokenService.GenerateJwtToken(user);
        return ServiceResult<TokenResponseDto>.Success(new TokenResponseDto { Token = token, Email = user.Email });
    }
}
