using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

namespace Dfe.ManageFreeSchoolProjects.ViewModels;

public class ProjectStatusViewModel
{
    public string ProjectId { get; set; }
    
    public ProjectStatus ProjectStatus { get; set; }
    
}