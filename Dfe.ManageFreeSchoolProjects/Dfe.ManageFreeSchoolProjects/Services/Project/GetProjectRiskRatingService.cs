using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IGetProjectRiskRatingService
    {
        Task<GetProjectRiskRatingResponse> Execute(string projectId);
    }

    public class GetProjectRiskRatingService : IGetProjectRiskRatingService
    {
        private readonly MfspApiClient _apiClient;

        public GetProjectRiskRatingService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetProjectRiskRatingResponse> Execute(string projectId)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}/risk-rating";

            var result = await _apiClient.Get<ApiSingleResponseV2<GetProjectRiskRatingResponse>>(endpoint);

            return result.Data;
        }
    }
}
