namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class ImpactAssessmentTask
{
    public bool? ImpactAssessment { get; set; }
    
    public bool? SavedToWorkplaces { get; set; }
    
    public DateTime? Section9LetterDateSent { get; set; }
}