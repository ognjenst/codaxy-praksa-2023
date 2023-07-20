using Microsoft.EntityFrameworkCore;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SOCDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
