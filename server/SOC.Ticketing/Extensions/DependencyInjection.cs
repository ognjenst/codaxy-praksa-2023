using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOC.Ticketing.OptionsSetup;
using SOC.Ticketing.Services;

namespace SOC.Ticketing.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterOptions();
        services.AddScoped<ITicketingService, TicketingService>();

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<TicketingOptionsSetup>();

        return services;
    }
}
