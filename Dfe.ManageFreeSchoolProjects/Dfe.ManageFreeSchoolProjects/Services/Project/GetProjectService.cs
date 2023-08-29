using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IGetProjectService
    {
        public Task<GetProjectResponse> Execute(string projectId);
    }

    public class GetProjectService : IGetProjectService
    {
        private readonly MfspApiClient _apiClient;

        public GetProjectService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetProjectResponse> Execute(string projectId)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}";

            var result = await _apiClient.Get<ApiSingleResponseV2<GetProjectResponse>>(endpoint);

            return result.Data;
        }
    }
}
