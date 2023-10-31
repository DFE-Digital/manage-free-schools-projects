using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IGetProjectRiskRatingService
    {
        Task<GetProjectRiskResponse> Execute(string projectId);
    }

    public class GetProjectRiskRatingService : IGetProjectRiskRatingService
    {
        private readonly MfspApiClient _apiClient;

        public GetProjectRiskRatingService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetProjectRiskResponse> Execute(string projectId)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}/risk-rating";

            var result = await _apiClient.Get<ApiSingleResponseV2<GetProjectRiskResponse>>(endpoint);

            return result.Data;
        }
    }
}
