using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Dashboard
{
    public interface IGetDashboardByUserService
    {
        public Task<List<GetDashboardByUserResponse>> Execute(string userId);
    }

    public class GetDashboardByUserService : IGetDashboardByUserService
    {
        private MfspApiClient _apiClient;

        public GetDashboardByUserService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<GetDashboardByUserResponse>> Execute(string userId)
        {
            var endpoint = $"/api/v1/client/dashboard/byuser/{userId}";

            var result = await _apiClient.Get<ApiListWrapper<GetDashboardByUserResponse>>(endpoint);

            return result.Data.ToList();
        }
    }
}
