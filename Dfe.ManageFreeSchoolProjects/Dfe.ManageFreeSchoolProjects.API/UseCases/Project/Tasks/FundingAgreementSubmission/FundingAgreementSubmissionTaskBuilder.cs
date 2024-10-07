using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementSubmission
{
    public static class FundingAgreementSubmissionTaskBuilder
    {
        public static FundingAgreementSubmissionTask Build(Milestones milestones)
        {
            if (milestones == null)
            {
                return new FundingAgreementSubmissionTask();
            }

            return new FundingAgreementSubmissionTask()
            {
                DraftedFundingAgreementSubmission = milestones.FsgPreOpeningMilestonesMfadDraftedFaSubmission,
                RegionalDirectorSignedOffFundingAgreementSubmission = milestones.FsgPreOpeningMilestonesMfadRegionalDirectorSignedOffFaSubmission,
                MinisterSignedOffFundingAgreementSubmission = milestones.FsgPreOpeningMilestonesMfadMinisterSignedOffFaSubmission,
                IncludedSignedOffImpactAssessment = milestones.FsgPreOpeningMilestonesMfadIncludedSignedOffImpactAssessmentFaSubmission,
                SavedFundingAgreementSubmissionInWorkplacesFolder = milestones.FsgPreOpeningMilestonesMfadSavedFaSubmissionInWorkplacesFolder
            };

        }
    }
}
