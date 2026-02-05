using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Auth;

namespace RentARide.Application.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResult<TokenResponseDto>> RegisterAsync(RegisterDto registerDto);
    Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto loginDto);
}
