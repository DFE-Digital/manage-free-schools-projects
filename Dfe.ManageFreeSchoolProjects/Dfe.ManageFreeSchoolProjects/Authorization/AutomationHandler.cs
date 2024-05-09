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
                Console.Write("bypassdebug cypress secret is not present");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(authHeader))
            {
                Console.Write("bypassdebug authKey is not present in header");
                return false;
            }

            Console.Write("bypassdebug auth header should be here >>> " + authHeader + " <<<  usersecret should be here >>> " + secret +" <<!!!!endofdebugmessage!!!  ");
            //return authHeader == secret;
            return true;
        }
    }
}
