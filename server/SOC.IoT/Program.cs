using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConductorSharp.Engine.Extensions;
using ConductorSharp.Engine.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SOC.IoT.Base;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Extensions;
using SOC.IoT.Handler;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(
        (hosting, config) =>
        {
            config.AddJsonFile("appsettings.json", true);
            if (hosting.HostingEnvironment.IsDevelopment())
            {
                config.AddJsonFile("appsettings.Development.json", true);
            }
        }
    )
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices(
        (context, services) =>
        {
            services.RegisterServices(context.Configuration);
            services.AddIoTServices();
        }
    )
    .ConfigureContainer<ContainerBuilder>(
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

            builder.RegisterWorkerTask<DeviceHandler>();
            builder.RegisterWorkerTask<DetectionHandler>();
            builder.RegisterWorkerTask<CheckTimeHandler>();
            builder.RegisterWorkerTask<TemperatureColorHandler>();
            builder.RegisterWorkerTask<CheckIoTStatesHandler>();
        }
    );

using var host = builder.Build();

host.Services.GetRequiredService<IStartupService>();

await Task.Delay(2000);

await host.RunAsync();
