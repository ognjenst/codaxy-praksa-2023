using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SOC.Intelligence.OptionsSetup;
using Microsoft.Extensions.Logging;
using SOC.Intelligence.Services;

namespace SOC.Intelligence.Extensions;

public static class DependencyInjection
{
	public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.RegisterOptions();
		services.AddScoped<IIntelligenceService, IntelligenceService>();

		

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
		services.ConfigureOptions<IntelligenceOptionsSetup>();

		return services;
	}
}
