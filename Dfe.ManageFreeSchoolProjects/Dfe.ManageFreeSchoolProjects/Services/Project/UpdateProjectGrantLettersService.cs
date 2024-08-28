using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;

namespace Dfe.ManageFreeSchoolProjects.Services.Project;

public interface IUpdateProjectGrantLettersService
{
    public Task Execute(string projectId, GrantLetters grantLetters);
}

public class UpdateProjectGrantLettersService(MfspApiClient apiClient) : IUpdateProjectGrantLettersService
{
    public async Task Execute(string projectId, GrantLetters grantLetters)
    {
        var endpoint = $"/api/v1/client/projects/{projectId}/payments";
        await apiClient.Put<GrantLetters, ApiSingleResponseV2<object>>(endpoint, grantLetters);
    }
}
