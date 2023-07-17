using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConductorSharp.Engine.Extensions;
using ConductorSharp.Engine.Health;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SOC.Scanning.Extensions;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(
        (hosting, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true);
        }
    )
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices(
        (context, services) =>
        {
            services.RegisterServices(context.Configuration);
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

            builder.RegisterMediatR(typeof(Program).Assembly);
        }
    );

using var host = builder.Build();

await host.RunAsync();
