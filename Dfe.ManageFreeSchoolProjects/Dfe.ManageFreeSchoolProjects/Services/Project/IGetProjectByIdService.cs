using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IGetProjectByIdService
    {
        public Task<ProjectResponse> GetProject(string ProjectID);
    }
}
