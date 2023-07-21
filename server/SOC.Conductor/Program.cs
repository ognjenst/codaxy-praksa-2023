using Microsoft.EntityFrameworkCore;
using Serilog;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Extensions;
using System.Reflection;
using SOC.Conductor.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddDbContext<SOCDbContext>(
	options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db"))
);


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

app.Run();
