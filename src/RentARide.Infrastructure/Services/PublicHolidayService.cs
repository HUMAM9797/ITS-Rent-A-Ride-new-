using RentARide.Application.Services.Interfaces;
using RentARide.Domain.Interfaces;
using RentARide.Infrastructure.ExternalAPIs;

namespace RentARide.Infrastructure.Services;

public class PublicHolidayService : IPublicHolidayService
{
    private readonly NagerDateApiClient _apiClient;
    private readonly ICacheService _cacheService;

    public PublicHolidayService(NagerDateApiClient apiClient, ICacheService cacheService)
    {
        _apiClient = apiClient;
        _cacheService = cacheService;
    }

    public async Task<bool> IsPublicHolidayAsync(DateTime date, string countryCode = "US")
    {
        var cacheKey = $"holidays_{countryCode}_{date.Year}";

        var holidays = _cacheService.Get<List<DateTime>>(cacheKey);

        if (holidays == null)
        {
            holidays = await _apiClient.GetPublicHolidaysAsync(date.Year, countryCode);
            _cacheService.Set(cacheKey, holidays, TimeSpan.FromDays(30));
        }

        return holidays.Any(h => h.Date == date.Date);
    }

    public async Task<List<DateTime>> GetPublicHolidaysAsync(int year, string countryCode = "US")
    {
        var cacheKey = $"holidays_{countryCode}_{year}";

        var holidays = _cacheService.Get<List<DateTime>>(cacheKey);

        if (holidays == null)
        {
            holidays = await _apiClient.GetPublicHolidaysAsync(year, countryCode);
            _cacheService.Set(cacheKey, holidays, TimeSpan.FromDays(30));
        }

        return holidays;
    }
}
