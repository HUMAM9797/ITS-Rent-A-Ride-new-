namespace RentARide.Application.Services.Interfaces;

public interface IPublicHolidayService
{
    Task<bool> IsPublicHolidayAsync(DateTime date, string countryCode = "US");
    Task<List<DateTime>> GetPublicHolidaysAsync(int year, string countryCode = "US");
}
