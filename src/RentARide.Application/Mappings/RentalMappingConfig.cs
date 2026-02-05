using Mapster;
using RentARide.Application.DTOs.Rental;
using RentARide.Domain.Entities;

namespace RentARide.Application.Mappings;

public class RentalMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Rental, RentalDto>()
            .Map(dest => dest.VehicleModel, src => src.Vehicle.Model)
            .Map(dest => dest.UserFullName, src => $"{src.User.FirstName} {src.User.LastName}");

        config.NewConfig<Rental, RentalHistoryDto>()
            .Map(dest => dest.RentalId, src => src.Id)
            .Map(dest => dest.VehicleModel, src => src.Vehicle.Model);

        config.NewConfig<Amenity, AmenityDto>();
    }
}
