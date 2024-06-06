using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

namespace Dfe.ManageFreeSchoolProjects.Services.Project;

public interface IUpdateProjectStatusService
{
    public Task Execute(string projectId,UpdateProjectStatusRequest request);
}

public class UpdateProjectStatusService : IUpdateProjectStatusService
{
    private readonly MfspApiClient _apiClient;
    
    
    public UpdateProjectStatusService(MfspApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    
    
    public async Task Execute(string projectId, UpdateProjectStatusRequest request)
    {
        var endpoint = $"/api/v1/client/updateprojectstatus={projectId}";

        await _apiClient.Patch<UpdateProjectStatusRequest,object>(endpoint, request);
        
    }
}