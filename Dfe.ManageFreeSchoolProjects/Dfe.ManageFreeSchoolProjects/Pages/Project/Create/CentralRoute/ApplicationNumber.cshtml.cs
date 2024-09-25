using Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;
using Dfe.ManageFreeSchoolProjects.Services.Project;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.CentralRoute;

public class ApplicationNumber(ICreateProjectCache createProjectCache) : CreateProjectBaseModel(createProjectCache)
{
    public void OnGet()
    {
        
    }
}