using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreement
{
	public static class FundingAgreementTaskBuilder
	{
		public static FundingAgreementTask Build(Milestones milestones)
		{
			if (milestones == null)
			{
				return new FundingAgreementTask();
			}

			return new FundingAgreementTask()
			{
				TailoredTheFundingAgreement = milestones.FsgPreOpeningMilestonesMfadTailoredAModelFundingAgreement,
				SharedFAWithTheTrust = milestones.FsgPreOpeningMilestonesMfadSharedFaWithTheTrust,
				TrustHasSignedTheFA = milestones.FsgPreOpeningMilestonesMfadTrustAgreesWithModelFa,
				DateTheTrustSignedFA = milestones.FsgPreOpeningMilestonesMfadActualDateOfCompletion,
				ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf = milestones.FsgPreOpeningMilestonesFaForecastDate,
                DateFAWasSigned = milestones.FsgPreOpeningMilestonesFaActualDateOfCompletion,
                SavedFADocumentsInWorkplacesFolder = milestones.FsgPreOpeningMilestonesMfadSavedFaDocumentsInWorkspacesFolder,
				
            };
		}
	}
}
