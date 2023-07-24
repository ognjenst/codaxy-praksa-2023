using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Configuration;
using ConductorSharp.Engine.Extensions;
using ConductorSharp.Engine.Health;
using MediatR.Extensions.Autofac.DependencyInjection;
using SOC.Intelligence.Extensions;
using SOC.Intelligence.Services;
using Microsoft.Extensions.DependencyInjection;
using SOC.Intelligence.Handler;

var builder = Host.CreateDefaultBuilder()
	.ConfigureAppConfiguration(
		(hosting, config) =>
		{
			config.AddJsonFile("appsettings.Development.json");
		}
	)
	.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureServices(
		(context, services) =>
		{
			services.RegisterServices(context.Configuration);
		}
	);/*
	ConfigureContainer<ContainerBuilder>(
		(context, builder) =>
		{
			builder
				.AddConductorSharp(
					baseUrl: context.Configuration.GetValue<string>("Conductor:BaseUrl"),
					apiPath: context.Configuration.GetValue<string>("Conductor:ApiUrl")
				)
				.AddExecutionManager(
					maxConcurrentWorkers: context.Configuration.GetValue<int>(
						"Conductor:MaxConcurrentWorkers"
					),
					sleepInterval: context.Configuration.GetValue<int>("Conductor:SleepInterval"),
					longPollInterval: context.Configuration.GetValue<int>(
						"Conductor:LongPollInterval"
					),
					domain: context.Configuration.GetValue<string>("Conductor:WorkerDomain"),
					handlerAssemblies: typeof(Program).Assembly
				)
				.SetHealthCheckService<FileHealthService>()
				.AddPipelines(pipelines =>
				{
					pipelines.AddContextLogging();
					pipelines.AddRequestResponseLogging();
					pipelines.AddValidation();
				});

			builder.RegisterWorkerTask<IntelligenceHandler>();

			builder.RegisterMediatR(typeof(Program).Assembly);
		}
	);*/

using var host = builder.Build();

var service = host.Services.GetRequiredService<IIntelligenceService>();
service.CheckEndpoint("192.210.175.159", "90");


await host.RunAsync();