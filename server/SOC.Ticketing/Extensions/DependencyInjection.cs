using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SOC.Ticketing.OptionsSetup;
using SOC.Ticketing.Services;

namespace SOC.Ticketing.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterOptions();
        services.AddScoped<ITicketingService, TicketingService>();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel
        .Debug()
        .MinimumLevel
        .Override("Microsoft", LogEventLevel.Information)
        .WriteTo
        .Console()
        .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<TicketingOptionsSetup>();

        return services;
    }
}
