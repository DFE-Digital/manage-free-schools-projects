using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IGetProjectsByUserService
    {
        public Task<ProjectResponse[]> GetProjects(string CreatedBy);
    }
}
