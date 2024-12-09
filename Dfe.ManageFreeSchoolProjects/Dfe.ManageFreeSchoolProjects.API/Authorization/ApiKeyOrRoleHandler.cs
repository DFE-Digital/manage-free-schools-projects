using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.API.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

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
