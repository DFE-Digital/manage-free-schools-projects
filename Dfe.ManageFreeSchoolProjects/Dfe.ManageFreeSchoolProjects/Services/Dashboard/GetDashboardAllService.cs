using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Dashboard
{
    public interface IGetDashboardAllService
    {
        public Task<List<GetDashboardResponse>> Execute();
    }

    public class GetDashboardAllService : IGetDashboardAllService
    {
        private readonly MfspApiClient _apiClient;

        public GetDashboardAllService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<GetDashboardResponse>> Execute()
        {
            var endpoint = $"/api/v1/client/dashboard/all";

            var result = await _apiClient.Get<ApiListWrapper<GetDashboardResponse>>(endpoint);

            return result.Data.ToList();
        }

    }
}
