using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Generated;
using SOC.Conductor.Options;
using SOC.Conductor.OptionsSetup;
using SOC.Conductor.Repositories;
using SOC.Conductor.Services;

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

		services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IWorkflowBuilderService, WorkflowBuilderService>();

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

	public static WebApplication MigrateDatabase(this WebApplication application)
	{
		using (var scope = application.Services.CreateScope())
		{
			using var dbContext = scope.ServiceProvider.GetRequiredService<SOCDbContext>();

			try
			{
				dbContext.Database.Migrate();
			}
			catch (Exception ex)
			{
				throw new Exception(
					$"Failed applying DB migrations for DB {nameof(SOCDbContext)}",
					ex
				);
			}
		}

		return application;
	}
}
