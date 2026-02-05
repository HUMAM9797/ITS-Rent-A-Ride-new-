using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Rental;

namespace RentARide.Application.Services.Interfaces;

public interface IRentalService
{
    Task<ServiceResult<RentalDto>> CreateRentalAsync(Guid userId, CreateRentalDto createRentalDto);
    Task<ServiceResult<IEnumerable<RentalHistoryDto>>> GetUserRentalHistoryAsync(Guid userId);
}
