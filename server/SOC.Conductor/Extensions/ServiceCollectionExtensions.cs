using IoT.Conductor.Services;
using Microsoft.Extensions.Options;
using SOC.Conductor.Options;
using SOC.Conductor.OptionsSetup;

namespace SOC.Conductor.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddRouting(opt => opt.LowercaseUrls = true);
        services.AddMediatR(
            config => config.RegisterServicesFromAssembly(typeof(Program).Assembly)
        );

        services.RegisterOptions();
        services.RegisterConductorHttpClients();
        services.AddHttpClientConfig();

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<ConductorOptionsSetup>();

        return services;
    }

    private static IServiceCollection RegisterConductorHttpClients(this IServiceCollection services)
    {
        services.AddScoped<IMetadataResourceClient, MetadataResourceClient>();
        services.AddScoped<IWorkflowResourceClient, WorkflowResourceClient>();

        return services;
    }

    private static IServiceCollection AddHttpClientConfig(this IServiceCollection services)
    {
        services.AddHttpClient<MetadataResourceClient>(
            (serviceProvider, client) =>
            {
                var conductorOptions = serviceProvider.GetRequiredService<
                    IOptions<ConductorOptions>
                >();

                client.BaseAddress = new Uri(conductorOptions.Value.ConductorUrl);
            }
        );

        services.AddHttpClient<WorkflowResourceClient>(
            (serviceProvider, client) =>
            {
                var conductorOptions = serviceProvider.GetRequiredService<
                    IOptions<ConductorOptions>
                >();

                client.BaseAddress = new Uri(conductorOptions.Value.ConductorUrl);
            }
        );

        return services;
    }
}
