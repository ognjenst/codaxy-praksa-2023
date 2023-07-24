﻿using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Configuration;
using ConductorSharp.Engine.Extensions;
using ConductorSharp.Engine.Health;
using MediatR.Extensions.Autofac.DependencyInjection;
using SOC.Ticketing.Extensions;
using SOC.Ticketing.Handler;
using Microsoft.Extensions.DependencyInjection;
using SOC.Ticketing.Services;
using SOC.Ticketing.Generated;
using MediatR;
using System.ComponentModel.Design;

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
    ).
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

            builder.RegisterWorkerTask<TicketingHandler>();

            builder.RegisterMediatR(typeof(Program).Assembly);
        }
    );

using var host = builder.Build();


var mediator = host.Services.GetRequiredService<IMediator>();
var ticketingRequest = new TicketingRequest
{
    Message = "My message5",
    Title = "New incident5",
    Severity = InputCreateCaseSeverity._3
};

var service = host.Services.GetRequiredService<ITicketingService>();

try
{
    await mediator.Send(ticketingRequest);
}catch(Exception ex)
{
    Console.WriteLine(ex);
}

await host.RunAsync();