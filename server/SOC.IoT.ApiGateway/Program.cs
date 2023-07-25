using SOC.IoT.Base.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using SOC.IoT.Base;
using SOC.IoT.ApiGateway.Hubs;
using SOC.IoT.ApiGateway.Controllers.Examples;
using SOC.IoT.ApiGateway.Middleware;
using SOC.IoT.ApiGateway.Extensions;
using Serilog;
using Microsoft.EntityFrameworkCore;
using SOC.IoT.ApiGateway.Entities.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SOCIoTDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opts.IncludeXmlComments(xmlPath);

    opts.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<DevicesExample>();
builder.Services.AddLogging();
builder.Services.AddSignalR();

builder.Services.AddIoTServices();

builder.Services.RegisterServices();
// Configure Serilog
builder.Host.UseSerilog(
	(context, config) =>
	{
		config.WriteTo.Console();
	}
);

// Configure Serilog
builder.Host.UseSerilog(
    (context, config) =>
    {
        config.WriteTo.Console();
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseMiddleware<GlobalExceptionMiddleware>();


// Disable CORS
app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:5544")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();
app.Services.GetRequiredService<IStartupService>();

app.MapHub<DevicesHub>("/api/hubs/devices");

//app.MigrateDatabase();

app.Run();
