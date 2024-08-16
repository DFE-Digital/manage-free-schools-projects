namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class ImpactAssessmentTask
{
    public bool? ImpactAssessment { get; set; }
    
    public bool? SavedToWorkplaces { get; set; }
    
    public bool? SentSection9LetterToLocalAuthority { get; set; }
    
    public DateTime? Section9LetterDateSent { get; set; }
}