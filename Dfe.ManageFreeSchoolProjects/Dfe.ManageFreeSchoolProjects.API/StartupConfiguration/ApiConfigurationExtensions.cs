using System.Security.Claims;
using DfE.CoreLibs.Security;
using DfE.CoreLibs.Security.Authorization;
using Dfe.ManageFreeSchoolProjects.API.Authorization;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Dfe.ManageFreeSchoolProjects.API.StartupConfiguration;

public static class ApiConfigurationExtensions
{
	public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient("DefaultClient");

		services.AddControllers();
		services.AddApiVersioning(config =>
		{
			config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
			config.AssumeDefaultVersionWhenUnspecified = true;
			config.ReportApiVersions = true;
		});
		services.AddVersionedApiExplorer(setup =>
		{
			// ReSharper disable once StringLiteralTypo
			setup.GroupNameFormat = "'v'VVV";
			setup.SubstituteApiVersionInUrl = true;
		});

        services.AddApplicationAuthorization(configuration, new Dictionary<string, Action<AuthorizationPolicyBuilder>>
        {
            { "Reports", policy =>
                {
                    policy.Requirements.Add(new ApiKeyOrRoleRequirement("user"));
                }
            }
        });

        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        services.AddCustomJwtAuthentication(configuration, JwtBearerDefaults.AuthenticationScheme, authenticationBuilder, new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                logger.LogError(context.Exception, "Authentication failed.");
                return Task.CompletedTask;
            },

            OnMessageReceived = context =>
            {
                if (context.HttpContext.Request.Headers.TryGetValue("ApiKey", out _))
                {
                    context.NoResult();
                    return Task.CompletedTask;
                }

                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                logger.LogInformation("Authentication message received.");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                logger.LogInformation("Token validated successfully.");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                logger.LogWarning("Authentication challenge triggered. Scheme: {Scheme}", context.Scheme.Name);
                // Suppress the default challenge behavior to prevent redirection
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"error\":\"Unauthorized\"}");
            }
        });

        services.AddSingleton<IAuthorizationHandler, ApiKeyOrRoleHandler>();

        services.AddSwaggerGen();
		services.ConfigureOptions<SwaggerOptions>();

		return services;
	}
}