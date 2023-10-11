using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Services.Tasks;

public interface IUpdateTaskStatusService
{
}

public class UpdateTaskStatusService : IUpdateTaskStatusService
{
    private readonly MfspApiClient _apiClient;

    public UpdateTaskStatusService(MfspApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task Execute(string projectId, string taskName)
    {
        var endpoint = $"/api/v1/{projectId}/task/status";

        try
        {
            var response = await _apiClient.Patch<object, object>(endpoint, new { taskName = taskName });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

