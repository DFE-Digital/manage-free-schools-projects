using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IUpdateProjectTaskService
    {
        public Task Execute(string projectId, UpdateProjectTasksRequest request);
    }

    public class UpdateProjectTaskService : IUpdateProjectTaskService
    {
        private readonly MfspApiClient _apiClient;

        public UpdateProjectTaskService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Execute(string projectId, UpdateProjectTasksRequest request)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}/tasks";

            await _apiClient.Patch<UpdateProjectTasksRequest, object>(endpoint, request);
        }
    }
}
