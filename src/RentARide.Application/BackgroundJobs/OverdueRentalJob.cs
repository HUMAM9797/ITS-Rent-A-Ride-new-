using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentARide.Domain.Interfaces;

namespace RentARide.Application.BackgroundJobs;

public class OverdueRentalJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OverdueRentalJob> _logger;

    public OverdueRentalJob(IServiceProvider serviceProvider, ILogger<OverdueRentalJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("OverdueRentalJob running at: {time}", DateTimeOffset.Now);

            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var activeRentals = await unitOfWork.Rentals.GetActiveRentalsAsync();

                var overdueRentals = activeRentals.Where(r => r.EndDate < DateTime.UtcNow).ToList();

                foreach (var rental in overdueRentals)
                {
                    _logger.LogWarning("Rental {RentalId} is OVERDUE! (End Date: {EndDate})", rental.Id, rental.EndDate);
                }
            }

            // Wait for 1 hour
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
