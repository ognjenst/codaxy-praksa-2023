using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SOC.Conductor.Client.Generated;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Options;
using SOC.IoT.ApiGateway.OptionsSetup;

namespace SOC.IoT.ApiGateway.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.RegisterOptions();
        services.RegisterHttpClients();

        services.AddScoped<IWorkflowsService, WorkflowsService>(
            (serviceProvider) =>
            {
                var conductorClientOptions = serviceProvider.GetRequiredService<
                    IOptions<ConductorClientOpitons>
                >();

                var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

                return new WorkflowsService(
                    baseUrl: conductorClientOptions.Value.BaseUrl,
                    clientFactory.CreateClient()
                );
            }
        );
        return services;
    }

    private static IServiceCollection RegisterHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<WorkflowsService>();
        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<ConductorClientOpitonsSetup>();
        return services;
    }

    public static WebApplication MigrateDatabase(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            using var dbContext =
                scope.ServiceProvider.GetRequiredService<SOCIoTDbContext>();

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed applying DB migrations for DB {nameof(SOCIoTDbContext)}",
                    ex
                );
            }
        }

        return application;
    }
}
