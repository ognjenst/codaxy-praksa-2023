using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOC.Scanning.OptionsSetup;

namespace SOC.Scanning.Extensions;

public static class DependencyInjection
{
    

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<SshOptionsSetup>();

        return services;
    }
}
