using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;

namespace Dfe.ManageFreeSchoolProjects.Services.Project;

public interface IGrantLettersService
{
    Task UpdateGrantLetters(string projectId, ProjectGrantLetters updatedOrNewGrantLetter);
    Task<ProjectGrantLetters> Get(string projectId);

    Task UpdateVariationLetter(string projectId, GrantVariationLetter updateOrNewGrantLetter);
}

public class GrantLettersService(MfspApiClient apiClient) : IGrantLettersService
{
    public async Task UpdateGrantLetters(string projectId, ProjectGrantLetters updatedOrNewGrantLetter)
    {
        var endpoint = $"/api/v1/client/projects/{projectId}/grant-letters";
        await apiClient.Put<ProjectGrantLetters, ApiSingleResponseV2<object>>(endpoint, updatedOrNewGrantLetter);
    }

    public async Task<ProjectGrantLetters> Get(string projectId)
    {
        var endpoint = $"/api/v1/client/projects/{projectId}/grant-letters";
        var response = await apiClient.Get<ApiSingleResponseV2<ProjectGrantLetters>>(endpoint);
        return response.Data;
    }

    public Task UpdateVariationLetter(string projectId, GrantVariationLetter updateOrNewGrantLetter)
    {
        throw new System.NotImplementedException();
    }
}
