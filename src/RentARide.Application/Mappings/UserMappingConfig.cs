using Mapster;
using RentARide.Application.DTOs.Auth;
using RentARide.Domain.Entities;

namespace RentARide.Application.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterDto, User>()
            .Map(dest => dest.PasswordHash, src => string.Empty); // Will be hashed in service
    }
}
