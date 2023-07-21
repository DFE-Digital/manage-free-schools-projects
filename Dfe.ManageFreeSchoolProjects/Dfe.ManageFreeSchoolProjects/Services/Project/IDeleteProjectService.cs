using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IDeleteProjectService
    {
        Task<long> DeleteProject(string ProjectID);
    }
}
