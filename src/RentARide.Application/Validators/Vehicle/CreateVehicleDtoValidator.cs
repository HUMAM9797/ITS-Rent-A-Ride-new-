using FluentValidation;
using RentARide.Application.DTOs.Vehicle;

namespace RentARide.Application.Validators.Vehicle;

public class CreateVehicleDtoValidator : AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleDtoValidator()
    {
        RuleFor(x => x.Model).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Year).InclusiveBetween(1900, DateTime.Now.Year + 1);
        RuleFor(x => x.LicensePlate).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DailyPrice).GreaterThan(0);
        RuleFor(x => x.VehicleTypeId).NotEmpty();
    }
}
