using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;

namespace Dfe.ManageFreeSchoolProjects.Authorization
{
    public static class AutomationHandler
    {
        public static bool ClientSecretHeaderValid(IHostEnvironment hostEnvironment,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            //Header authorisation not applicable for production
            if (hostEnvironment.IsProduction())
            {
                return false;
            }

            //Allow client secret in header
            var authHeader = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString()?
                .Replace("Bearer ", string.Empty);

            var secret = configuration.GetValue<string>("CypressTestSecret");

            if (string.IsNullOrWhiteSpace(secret))
            {
                Console.Write(secret + "test666 secret is not present in header");
                return false;
            }

            Console.Write("test666" + authHeader + "authheader<" + secret + "<secret");
            return authHeader == secret;
        }
    }
}
