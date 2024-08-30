using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;

namespace Dfe.ManageFreeSchoolProjects.Services.Project;

public interface IGrantLettersService
{
    Task Update(string projectId, PdgGrantLetters pdgGrantLetters);
    Task<ProjectGrantLetters> Get(string projectId);
}

public class GrantLettersService(MfspApiClient apiClient) : IGrantLettersService
{
    public async Task Update(string projectId, PdgGrantLetters pdgGrantLetters)
    {
        var endpoint = $"/api/v1/client/projects/{projectId}/grant-letters";
        await apiClient.Put<PdgGrantLetters, ApiSingleResponseV2<object>>(endpoint, pdgGrantLetters);
    }

    public async Task<ProjectGrantLetters> Get(string projectId)
    {
        var endpoint = $"/api/v1/client/projects/{projectId}/grant-letters";
        return await apiClient.Get<ProjectGrantLetters>(endpoint);
    }
}
