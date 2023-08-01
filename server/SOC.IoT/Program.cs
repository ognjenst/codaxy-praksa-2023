using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConductorSharp.Engine.Extensions;
using ConductorSharp.Engine.Health;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SOC.IoT.Extensions;
using SOC.IoT.Handler;
using SOC.IoT.Services;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(
        (hosting, config) => {
            config.AddJsonFile("appsettings.json", true);
            config.AddJsonFile("appsettings.Development.json", true);
        }
    )
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices(
        (context, services) => {
            services.RegisterServices(context.Configuration);
        }
    )
    .ConfigureContainer<ContainerBuilder>(
        (context, builder) => {
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
                .AddPipelines(pipelines => {
                    pipelines.AddContextLogging();
                    pipelines.AddRequestResponseLogging();
                    pipelines.AddValidation();
                });

            builder.RegisterWorkerTask<DeviceHandler>();
			builder.RegisterWorkerTask<DetectionHandler>();
            builder.RegisterWorkerTask<CheckTimeHandler>();
		}
    );

using var host = builder.Build();

/*
var service = host.Services.GetRequiredService<IDeviceService>();
await service.LigthBulbInRepetitions(new DetectionRequest { DeviceId = "0x00158d0001dd7e46", X = 0.675, Y = 0.322 }, new CancellationToken());
*/
await host.RunAsync();
