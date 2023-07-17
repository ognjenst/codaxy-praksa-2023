using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOC.Scanning.OptionsSetup;

namespace SOC.Scanning.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(typeof(Program).Assembly);

        services.RegisterOptions();

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<SshOptionsSetup>();

        return services;
    }
}
