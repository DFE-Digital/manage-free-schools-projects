namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class EducationBriefTask
{

    public bool? TrustConfirmedPlansAndPoliciesInPlace { get; set; }
    public bool? CommissionedEEToReviewSafeguardingPolicy { get; set; }
    public bool? CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy { get; set; }
    public DateTime? DateEEReviewedEducationBrief { get; set; }
    public bool? SavedEESpecificationAndAdviceInWorkplaces { get; set; }
    public bool? SavedCopiesOfPlansAndPoliciesInWorkplaces { get; set; }
    public DateTime? DateTrustProvidedEducationBrief { get; set; }
}