using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IUpdateProjectSitesService
    {
        public Task Execute(string projectId, UpdateProjectSitesRequest request);
    }

    public class UpdateProjectSitesService : IUpdateProjectSitesService
    {
        private readonly MfspApiClient _apiClient;

        public UpdateProjectSitesService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Execute(string projectId, UpdateProjectSitesRequest request)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}/sites";

            await _apiClient.Patch<UpdateProjectSitesRequest, ApiSingleResponseV2<object>>(endpoint, request);
        }
    }
}
