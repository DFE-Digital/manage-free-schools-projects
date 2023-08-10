using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Dashboard
{
    public interface IGetDashboardByUserService
    {
        public Task<List<GetDashboardResponse>> Execute(string userId);
    }

    public interface IGetDashboardAllService
    {
        public Task<List<GetDashboardResponse>> Execute();
    }

    public class GetDashboardByUserService : IGetDashboardByUserService
    {
        private MfspApiClient _apiClient;

        public GetDashboardByUserService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<GetDashboardResponse>> Execute(string userId)
        {
            var endpoint = $"/api/v1/client/dashboard/byuser/{userId}";

            var result = await _apiClient.Get<ApiListWrapper<GetDashboardResponse>>(endpoint);

            return result.Data.ToList();
        }

    }

    public class GetDashboardAllService : IGetDashboardAllService
    {
        private MfspApiClient _apiClient;

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
