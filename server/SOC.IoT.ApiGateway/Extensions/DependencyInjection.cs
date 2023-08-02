using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOC.Conductor.Client.Generated;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Entities.Enums;
using SOC.IoT.ApiGateway.Options;
using SOC.IoT.ApiGateway.OptionsSetup;
using SOC.IoT.ApiGateway.Security;
using SOC.IoT.ApiGateway.Services;
using System.Text;

namespace SOC.IoT.ApiGateway.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.RegisterOptions();
        services.RegisterHttpClients();
        


        services.AddScoped<IWorkflowsClient, WorkflowsClient>(
            (serviceProvider) =>
            {
                var conductorClientOptions = serviceProvider.GetRequiredService<
                    IOptions<ConductorClientOpitons>
                >();

                var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

                return new WorkflowsClient(
                    baseUrl: conductorClientOptions.Value.BaseUrl,
                    clientFactory.CreateClient()
                );
            }
        );

        services.AddScoped<ITriggersClient, TriggersClient>(
            (serviceProvider) =>
            {
                var conductorClientOptions = serviceProvider.GetRequiredService<
                    IOptions<ConductorClientOpitons>
                >();

                var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

                return new TriggersClient(
                    baseUrl: conductorClientOptions.Value.BaseUrl,
                    clientFactory.CreateClient()
                );
            }
        );

        services.AddScoped<IAutomationClient, AutomationClient>(
            (serviceProvider) =>
            {
                var conductorClientOptions = serviceProvider.GetRequiredService<
                    IOptions<ConductorClientOpitons>
                >();

                var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

                return new AutomationClient(
                    baseUrl: conductorClientOptions.Value.BaseUrl,
                    clientFactory.CreateClient()
                );
            }
        );

        services.AddCors(
            opt =>
                opt.AddPolicy(
                    "CorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .WithMethods("GET", "PUT", "POST", "DELETE", "PATCH", "OPTIONS");
                    }
                )
        );

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
            options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("192.168.160.0"), 24));
            options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("10.0.0.0"), 8));

            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto
                | ForwardedHeaders.XForwardedHost;
        });

        return services;
    }

    private static IServiceCollection RegisterHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<WorkflowsClient>();
        services.AddHttpClient<ITriggersClient>();
        services.AddHttpClient<AutomationClient>();
        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<ConductorClientOpitonsSetup>();

        services.ConfigureOptions<JwtSecretSetup>();
        return services;
    }

    public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
		var jwtSecretKey = configuration.GetValue<string>("Jwt:Key");
        var issuer = configuration.GetValue<string>("Jwt:Issuer");
        var audience = configuration.GetValue<string>("Jwt:Audience");
        if (jwtSecretKey != null) 
        {
			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey));

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	            .AddJwtBearer(options =>
	            {
		            options.TokenValidationParameters = new TokenValidationParameters
		            {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
			            ValidateIssuerSigningKey = true,
			            IssuerSigningKey = key,
			            ValidateIssuer = true,
			            ValidateAudience = true,
			            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
			            ClockSkew = TimeSpan.Zero
		            };
	            });
		}
		return services;
    }

    public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var scope in PermissionData.Scopes)
            {
                foreach (var resource in PermissionData.Resources)
                {
                    options.AddPolicy($"{scope}-{resource}", policy =>
                    {
                        policy.Requirements.Add(new JwtRequirements($"{scope}-{resource}"));
                    });
                }
            }
        });
		return services;
    }

    public static WebApplication MigrateDatabase(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            using var dbContext = scope.ServiceProvider.GetRequiredService<SOCIoTDbContext>();

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed applying DB migrations for DB {nameof(SOCIoTDbContext)}",
                    ex
                );
            }
        }

        return application;
    }
}
