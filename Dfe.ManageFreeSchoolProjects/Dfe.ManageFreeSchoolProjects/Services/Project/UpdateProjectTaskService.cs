using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IUpdateProjectTaskService
    {
        public Task Execute(string projectId, UpdateProjectByTaskRequest request);
    }

    public class UpdateProjectTaskService : IUpdateProjectTaskService
    {
        private readonly MfspApiClient _apiClient;

        public UpdateProjectTaskService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Execute(string projectId, UpdateProjectByTaskRequest request)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}/tasks";

            await _apiClient.Patch<UpdateProjectByTaskRequest, object>(endpoint, request);
        }
    }
}
