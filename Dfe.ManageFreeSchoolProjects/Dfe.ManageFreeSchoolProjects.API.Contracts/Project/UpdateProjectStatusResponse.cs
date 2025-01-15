namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

public class UpdateProjectStatusResponse
{
    public ProjectStatus ProjectStatus { get; set; }

    public ProjectCancelledReason ProjectCancelledReason { get; set; }

    public ProjectWithdrawnReason ProjectWithdrawnReason { get; set; }

    public DateTime? CancelledDate { get; set; }
    
    public DateTime? ClosedDate { get; set; }
    
    public DateTime? WithdrawnDate { get; set; }
}
