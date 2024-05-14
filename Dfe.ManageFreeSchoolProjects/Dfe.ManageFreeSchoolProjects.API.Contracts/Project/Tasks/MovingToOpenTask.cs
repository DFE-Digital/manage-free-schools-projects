using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class MovingToOpenTask
{
    public bool? ProjectBriefToSfso{ get; set; }
        
    public bool? ProjectBriefToEducationEstates{ get; set; }
        
    public bool? ProjectBriefToNewDeliveryOfficer{ get; set; }
    
    public bool? SentEmailsToRelevantContacts { get; set; }
        
    public bool? SentEmailsToSchoolsPrinciple { get; set; }
        
    public bool? SavedToWorkplacesFolderProjectBrief { get; set; }
        
    public bool? SavedToWorkplacesFolderAnnexB { get; set; }
        
    public bool? SavedToWorkplacesFolderAnnexE { get; set; }
    public DateTime? ActualOpeningDate { get; set; }
}
