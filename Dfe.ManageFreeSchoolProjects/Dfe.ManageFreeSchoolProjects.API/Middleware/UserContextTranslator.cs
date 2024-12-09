using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Dfe.ManageFreeSchoolProjects.API.Middleware
{
    public class UserContextTranslator(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            if (context.Request.Headers.TryGetValue("ApiKey", out StringValues apiKeyHeader))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "ApiKeyUser"),
                    new Claim(ClaimTypes.Role, "user")
                };
                var identity = new ClaimsIdentity(claims, "ApiScheme");
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
            }
            await next(context);
        }

    }
}
