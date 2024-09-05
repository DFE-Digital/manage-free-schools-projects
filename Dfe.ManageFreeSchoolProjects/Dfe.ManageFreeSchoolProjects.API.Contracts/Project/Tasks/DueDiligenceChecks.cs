namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class DueDiligenceChecks
{
    public bool? ReceivedChairOfTrusteesCountersignedCertificate { get; set; }
    public bool? NonSpecialistChecksDoneOnAllTrustMembersAndTrustees { get; set; }
    public bool? RequestedCounterExtremismChecks { get; set; }
    public DateTime? DateWhenAllChecksWereCompleted { get; set; }
    public bool? SavedNonSpecialistChecksSpreadsheetInWorkplaces { get; set; }
    public bool? DeletedAnyCopiesOfChairsDBSCertificate { get; set; }
    public bool? DeletedEmailsContainingSuitabilityAndDeclarationForms { get; set; }
}