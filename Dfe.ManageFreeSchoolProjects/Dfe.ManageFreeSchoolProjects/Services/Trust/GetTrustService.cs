using System;
using System.Threading.Tasks;
using System.Net.Http;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Trust;
using Newtonsoft.Json;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using ConcernsCaseWork.Service.Base;
using System.Web;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.UserContext;

namespace Dfe.ManageFreeSchoolProjects.Services.Trust
{
    public interface IGetTrustService
    {
        public Task<TrustSearchResponseDto> GetTrustsByPagination(TrustSearch trustSearch, int maxRecordsPerPage);
    }

    public class GetProjectByTaskService : IGetTrustService
    {
        private readonly ICorrelationContext _correlationContext;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IClientUserInfoService _userInfoService;

        private string HttpClientName { get; init; } = "TramsClient";

        public async Task<TrustSearchResponseDto> GetTrustsByPagination(TrustSearch trustSearch, int maxRecordsPerPage)
        {
            Guard.Against.Null(trustSearch, nameof(trustSearch));
            Guard.Against.NegativeOrZero(maxRecordsPerPage, nameof(maxRecordsPerPage));

            try
            {
                // Create a request
                var endpoint = $"/v2/trusts?{BuildRequestUri(trustSearch, 10)}";
                var response = await GetByPagination<TrustSearchDto>(endpoint);

                //Combine the results of Trusts and CTC Searches
                List<TrustSearchDto> matches = new List<TrustSearchDto>();
                if (response.Data != null)
                {
                    matches.AddRange(response.Data);
                }

                return new TrustSearchResponseDto { NumberOfMatches = matches.Count, Trusts = matches };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<ApiListWrapper<T>> GetByPagination<T>(string endpoint, bool treatNoContentAsError = false) where T : class
        {
            Guard.Against.NullOrWhiteSpace(endpoint, nameof(endpoint));

            async Task<ApiListWrapper<T>> DoWork()
            {
                try
                {
                    // Create a request
                    var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

                    var client = _clientFactory.CreateClient(HttpClientName);

                    AddDefaultRequestHeaders(client, _correlationContext, _userInfoService);

                    // Execute request
                    var response = await client.SendAsync(request);

                    // Check status code
                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        if (treatNoContentAsError)
                        {
                            throw new ArgumentNullException(nameof(response.Content),
                                $"A GET from endpoint '{endpoint}' resulted in a NoContent response. Exception thrown because {nameof(treatNoContentAsError)} is 'true'");
                        }

                        return default;
                    }

                    // Read response content
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserialize content to POCO
                    return JsonConvert.DeserializeObject<ApiListWrapper<T>>(content);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return DoWork();
        }

        public string BuildRequestUri(TrustSearch trustSearch, int maxRecordsPerPage)
        {
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            if (!string.IsNullOrEmpty(trustSearch.GroupName))
            {
                queryParams.Add("groupName", trustSearch.GroupName);
            }
            if (int.TryParse(trustSearch.Ukprn, out _))
            {
                queryParams.Add("ukprn", trustSearch.Ukprn);
            }
            if (!string.IsNullOrEmpty(trustSearch.CompaniesHouseNumber))
            {
                queryParams.Add("companiesHouseNumber", trustSearch.CompaniesHouseNumber);
            }
            queryParams.Add("page", trustSearch.Page.ToString());
            queryParams.Add("count", maxRecordsPerPage.ToString());
            queryParams.Add("includeEstablishments", false.ToString());
            return HttpUtility.UrlEncode(queryParams.ToString());
        }

        public static void AddDefaultRequestHeaders(HttpClient httpClient, ICorrelationContext correlationContext, IClientUserInfoService userInfoService)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(correlationContext.HeaderKey, correlationContext.CorrelationId);

            userInfoService.AddUserInfoRequestHeaders(httpClient);

        }

    }

}

