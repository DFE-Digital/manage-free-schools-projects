namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class CommissionedExternalExpertTask
{
    public bool? CommissionedExternalExpertVisit { get; set; }
            
    public DateTime? ExternalExpertVisitDate { get; set; }
            
    public bool? SavedExternalExpertSpecsToWorkplacesFolder { get; set; }
}