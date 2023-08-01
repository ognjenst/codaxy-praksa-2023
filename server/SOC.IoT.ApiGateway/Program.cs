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
using SOC.IoT.ApiGateway.Services;
using Autofac.Core;
using SOC.IoT.ApiGateway.Options;
using Microsoft.AspNetCore.Authorization;
using SOC.IoT.ApiGateway.Handlers;
using SOC.IoT.ApiGateway.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SOC.IoT.ApiGateway.Security;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SOCIoTDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(Program).Assembly));


// Configure JWT authentication
var jwtSecretKey = builder.Configuration.GetValue<string>("Jwt:Key");
var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey));


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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => 
	{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
			    IssuerSigningKey = key,
				ValidateIssuer = false,
				ValidateAudience = false,
				// set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
				ClockSkew = TimeSpan.Zero
			};
    });

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Update-Workflow", policy =>
	{
		policy.Requirements.Add(new JwtRequirements("Update-Workflow")) ;
	});
	
	options.AddPolicy("Read-Workflow", policy =>
	{
		policy.Requirements.Add(new JwtRequirements("Read-Workflow"));
	});
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Services.GetRequiredService<IStartupService>();

app.MapHub<DevicesHub>("/api/hubs/devices");

app.MigrateDatabase();

app.Run();
