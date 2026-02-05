using RentARide.Domain.Entities;

namespace RentARide.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}
