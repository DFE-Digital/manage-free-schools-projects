using Ardalis.GuardClauses;
using Dfe.ManageFreeSchoolProjects.UserContext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using System.Linq;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserContextMiddleware> _logger;

    public UserContextMiddleware(RequestDelegate next, ILogger<UserContextMiddleware> logger)
    {
        _next = next;
        _logger = Guard.Against.Null(logger);
    }

    public Task Invoke(HttpContext httpContext, IClientUserInfoService userInfoService)
    {


        if (httpContext.User.Identity != null && IsPageRequest(httpContext.Request.Path))
        {

            userInfoService.SetPrincipal(httpContext.User);
            return _next(httpContext);
        }
        else
        {
            return _next(httpContext);
        }
    }

    private bool IsPageRequest(string path) => !path.StartsWith("/api/v1/");
}
