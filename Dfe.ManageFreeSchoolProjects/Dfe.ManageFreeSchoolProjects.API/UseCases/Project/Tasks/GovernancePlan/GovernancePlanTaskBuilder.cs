using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.GovernancePlan
{
	public static class GovernancePlanTaskBuilder
	{
		public static GovernancePlanTask Build(Milestones milestones)
		{
			if (milestones == null)
			{
				return new GovernancePlanTask();
			}

			bool? LinkToSavedDocumentExists = milestones.FsgPreOpeningMilestonesMi125LinkToSavedDocument is null ? null : milestones.FsgPreOpeningMilestonesMi125LinkToSavedDocument.Length > 0;
            bool? SavedDocumentsInWorkplacesFolder = milestones.DraftGovernancePlanDocumentsSavedInWorkplacesFolder ?? LinkToSavedDocumentExists;

            bool? FinalGovernancePlanAgreed = milestones.FsgPreOpeningMilestonesFgpaActualDateOfCompletion is null ? milestones.FinalGovernancePlanAgreed : true;

            return new GovernancePlanTask()
			{
				SavedDocumentsInWorkplacesFolder = SavedDocumentsInWorkplacesFolder,
                PlanAndAssessmentSharedWithEsfa = milestones.DraftGovernancePlanAndAssessmentSharedWithEsfa,
				PlanAndAssessmentSharedWithExpert = milestones.DraftGovernancePlanAndAssessmentSharedWithExpert,
				PlanAndAssessmentSharedWithLocalAuthority = milestones.GovernancePlanAndAssessmentSharedWithLocalAuthority,
				PlanAssessedUsingTemplate = milestones.DraftGovernancePlanAssessedUsingTemplate,
				PlanFedBackToTrust = milestones.DraftGovernancePlanFedBackToTrust,
				PlanReceivedFromTrust = milestones.DraftGovernancePlanReceivedFromTrust,
				DatePlanReceived = milestones.FsgPreOpeningMilestonesDgpActualDateOfCompletion,
				FinalGovernancePlanAgreed = FinalGovernancePlanAgreed,
				DatePlanAgreed = milestones.FsgPreOpeningMilestonesFgpaActualDateOfCompletion,
                Comments = milestones.FsgPreOpeningMilestonesMi60CommentsOnDecisionToApproveIfApplicable,
			};
		}
	}
}
