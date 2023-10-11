using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;

namespace Dfe.ManageFreeSchoolProjects.Services.Tasks;

public interface IGetTaskStatusService
{
    Task<ProjectTaskStatus> Execute(string projectId, string taskName);
}

public class GetTaskStatusService : IGetTaskStatusService
{
    private readonly MfspApiClient _apiClient;

    public GetTaskStatusService(MfspApiClient apiClient)
    {
        _apiClient = apiClient;
    }


    public async Task<ProjectTaskStatus> Execute(string projectId, string taskName)
    {
        var endpoint = $"/api/v1/{projectId}/task/status?taskName={taskName}";

        try
        {
            var response = await _apiClient.Get<ApiSingleResponseV2<TaskStatusResponse>>(endpoint);

            return response.Data.ProjectTaskStatus;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}