using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Dfe.ManageFreeSchoolProjects.Services
{
    public class MfspApiClient : ApiClient
    {
        public MfspApiClient(IHttpClientFactory clientFactory, ILogger<ApiClient> logger, string httpClientName = "MfspClient") : base(clientFactory, logger, httpClientName)
        {
            
        }
    }
}
