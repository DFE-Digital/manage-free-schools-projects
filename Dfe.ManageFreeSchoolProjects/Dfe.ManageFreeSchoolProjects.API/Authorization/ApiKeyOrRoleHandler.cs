using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.API.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace Dfe.ManageFreeSchoolProjects.API.Authorization
{
    [ExcludeFromCodeCoverage]
    public class ApiKeyOrRoleHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
     : AuthorizationHandler<ApiKeyOrRoleRequirement>
    {
        private const string ApiKeyHeaderName = "ApiKey";
        private readonly IConfiguration _configuration = configuration;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyOrRoleRequirement requirement)
        {
            // Check API Key
            if (httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(ApiKeyHeaderName, out StringValues apiKeyHeader))
            {
                var keyExists = _configuration
                    .GetSection("ManageFreeSchoolProjects:ApiKeys")
                    .AsEnumerable()
                    .Any(k => k.Value == apiKeyHeader);

                if (keyExists)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                if (httpContextAccessor.HttpContext.Request.Path.StartsWithSegments("/api/v1/construct"))
                {
                    var constructKeyExists = _configuration
                        .GetSection("ManageFreeSchoolProjects")
                        .GetValue<string>("ConstructApiKey");

                    if(apiKeyHeader == constructKeyExists)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                    
                }

                if (httpContextAccessor.HttpContext.Request.Path.StartsWithSegments("/api/v1/client/reports/sfa-export"))
                {
                    var sfaKeyExists = _configuration
                        .GetSection("ManageFreeSchoolProjects")
                        .GetValue<string>("ConstructApiKey");

                    if (apiKeyHeader == sfaKeyExists)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }

                }

            }

            // Check Role-based authorization
            if (context?.User != null && context.User.IsInRole(requirement.RolePolicy))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
