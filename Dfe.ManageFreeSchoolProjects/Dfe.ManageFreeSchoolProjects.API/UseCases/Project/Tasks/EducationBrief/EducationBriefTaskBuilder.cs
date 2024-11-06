using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.EducationBrief
{
    public static class EducationBriefTaskBuilder
    {
        public static EducationBriefTask Build(Milestones milestones)
        {
            return new EducationBriefTask()
            {
                TrustConfirmedPlansAndPoliciesInPlace = milestones?.FSGPreOpeningMilestonesEPPTrustConfirmedPlansAndPoliciesInPlace,
                CommissionedEEToReviewSafeguardingPolicy = milestones?.FSGPreOpeningMilestonesEPPCommissionedEEToReviewSafeguardingPolicy,
                CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy = milestones?.FSGPreOpeningMilestonesEPPCommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy,
                DateEEReviewedEducationBrief = milestones?.FSGPreOpeningMilestonesEPPDateEEReviewedEducationBrief,
                SavedEESpecificationAndAdviceInWorkplaces = milestones?.FSGPreOpeningMilestonesEPPSavedEESpecificationAndAdviceInWorkplaces,
                SavedCopiesOfPlansAndPoliciesInWorkplaces = milestones?.FSGPreOpeningMilestonesEPPSavedCopiesOfPlansAndPoliciesInWorkplaces,
                DateTrustProvidedEducationBrief = milestones?.FSGPreOpeningMilestonesDateTrustProvidedEducationBrief,
    };
        }
    }
}