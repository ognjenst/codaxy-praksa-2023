using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SOC.Scanning.Handler;
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

        // Configure Serilog
        Log.Logger = new LoggerConfiguration().MinimumLevel
            .Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders(); // Remove the default logging providers
            loggingBuilder.AddSerilog(); // Add Serilog as the logging provider
        });

        services.AddTransient<ScanIpAddressHandler>();

        // Register the Test class as a transient service
        // TODO: Delete, only testing purpose
        services.AddTransient<Test>();

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<SshOptionsSetup>();

        return services;
    }
}
