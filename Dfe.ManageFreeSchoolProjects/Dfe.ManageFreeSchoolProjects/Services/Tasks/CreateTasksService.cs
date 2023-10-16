using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Services.Tasks;

public interface ICreateTasksService
{
    
}

public class CreateTasksService : ICreateTasksService
{
    private readonly MfspApiClient _apiClient;

    public CreateTasksService(MfspApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task Execute(string projectId)
    {
        var endpoint = $"/api/v1/{projectId}/tasks/status";
        await _apiClient.Post<object, object>(endpoint, null);
    }
}