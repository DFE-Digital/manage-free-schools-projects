namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

public class UpdateProjectStatusResponse
{
    public ProjectStatus ProjectStatus { get; set; }
    
    public DateTime? CancelledDate { get; set; }
    
    public DateTime? ClosedDate { get; set; }
    
    public DateTime? WithdrawnDate { get; set; }
}
