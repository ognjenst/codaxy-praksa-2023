using Microsoft.EntityFrameworkCore;
using Serilog;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Extensions;
using SOC.IoT.Base;
using SOC.IoT.Base.Interfaces;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddDbContext<SOCDbContext>(
	options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db"))
);

builder.Services.AddIoTServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opts.IncludeXmlComments(xmlPath);
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Services.GetRequiredService<IStartupService>();

app.Run();
