using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarrentlyTheBestAPI.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class WypozyczenieCheckerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public WypozyczenieCheckerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await CheckWypozyczenia();

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            await CheckWypozyczenia();
        }
    }

    private async Task CheckWypozyczenia()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WypozyczenieDbContext>();

        var wypozyczeniaDoZakonczenia = context.Wypozyczenia
            .Where(w => !w.CzyZakonczone && w.DataZakonczenia <= DateTime.UtcNow)
            .ToList();

        foreach (var wypozyczenie in wypozyczeniaDoZakonczenia)
        {
            wypozyczenie.CzyZakonczone = true;
        }

        await context.SaveChangesAsync();
    }
}
