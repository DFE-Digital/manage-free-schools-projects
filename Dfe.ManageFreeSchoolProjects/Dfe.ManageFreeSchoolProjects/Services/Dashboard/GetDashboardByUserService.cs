using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Dashboard
{
    public interface IGetDashboardByUserService
    {
        public Task<List<GetDashboardByUserResponse>> GetDashboardByUser(string userId);
    }

    public class GetDashboardByUserService : IGetDashboardByUserService
    {
        private IHttpClientFactory _httpClientFactory;

        public GetDashboardByUserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GetDashboardByUserResponse>> GetDashboardByUser(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/client/dashboard/byuser/{userId}");
            var client = _httpClientFactory.CreateClient("MfspClient");

            try
            {
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var wrapper = JsonConvert.DeserializeObject<ApiListWrapper<GetDashboardByUserResponse>>(content);

                return wrapper.Data.ToList();
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, $"Error occured while trying to GetSRMAById");
                throw;
            }
        }
    }
}
