using Mapster;
using RentARide.Application.DTOs.Vehicle;
using RentARide.Domain.Entities;

namespace RentARide.Application.Mappings;

public class VehicleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Vehicle, VehicleDto>()
            .Map(dest => dest.VehicleTypeName, src => src.VehicleType.Name);

        config.NewConfig<CreateVehicleDto, Vehicle>();
        config.NewConfig<VehicleType, VehicleTypeDto>();
    }
}
