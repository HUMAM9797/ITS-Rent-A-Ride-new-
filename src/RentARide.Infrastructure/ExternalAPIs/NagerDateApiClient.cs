using System.Text.Json;

namespace RentARide.Infrastructure.ExternalAPIs;

public class NagerDateApiClient
{
    private readonly HttpClient _httpClient;

    public NagerDateApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://date.nager.at/api/v3/");
    }

    public async Task<List<DateTime>> GetPublicHolidaysAsync(int year, string countryCode)
    {
        var response = await _httpClient.GetAsync($"PublicHolidays/{year}/{countryCode}");

        if (!response.IsSuccessStatusCode)
        {
            return new List<DateTime>();
        }

        var content = await response.Content.ReadAsStringAsync();
        var holidays = JsonSerializer.Deserialize<List<PublicHolidayDto>>(content);

        return holidays?.Select(h => DateTime.Parse(h.Date)).ToList() ?? new List<DateTime>();
    }

    private class PublicHolidayDto
    {
        public string Date { get; set; } = string.Empty;
        public string LocalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
