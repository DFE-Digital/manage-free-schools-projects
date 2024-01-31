namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class KickOffMeetingTask
{
    public string FundingArrangementDetails { get; set; }
    
    public string RealisticYearOfOpening { get; set; }
    
    public DateTime? ProvisionalOpeningDate { get; set; }
    
    public string SharepointLink { get; set; }
}