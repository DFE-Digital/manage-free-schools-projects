using DfE.CoreLibs.Security.Interfaces;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.UserContext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Dfe.ManageFreeSchoolProjects.Services
{
    public class MfspApiClient : ApiClient
    {
        public MfspApiClient(
            IHttpClientFactory clientFactory, 
            ILogger<ApiClient> logger,
            IHttpContextAccessor httpContextAccessor,
            ICorrelationContext correlationContext,
            IClientUserInfoService clientUserInfoService,
            IUserTokenService apiTokenService,
            string httpClientName = "MfspClient") : base(clientFactory, logger, httpContextAccessor, httpClientName, correlationContext, clientUserInfoService, apiTokenService)
        {
            
        }
    }
}
