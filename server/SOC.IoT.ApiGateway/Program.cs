using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SOC.IoT.ApiGateway.Controllers.Examples;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Extensions;
using SOC.IoT.ApiGateway.Hubs;
using SOC.IoT.ApiGateway.Middleware;
using SOC.IoT.ApiGateway.Security;
using SOC.IoT.ApiGateway.Services;
using SOC.IoT.Base;
using SOC.IoT.Base.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<SOCIoTDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db"))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SOCIoTDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db"))
);

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

builder.Services.AddHostedService<DevicesBackgroundService>();

builder.Services.AddIoTServices();

builder.Services.RegisterServices();
builder.Services.AddScoped<IUserService, UserService>();

// Configure authentication and authorization
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterAuthorization();

// ...

builder.Services.AddScoped<IAuthorizationHandler, JwtAuthorizationHandler>();

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

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

// Disable CORS
//app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Services.GetRequiredService<IStartupService>();

app.MapHub<DevicesHub>("/api/hubs/devices");

app.MigrateDatabase();

app.Run();
