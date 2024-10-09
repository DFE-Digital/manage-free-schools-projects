using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinalFinancePlan
{
    public static class FinalFinancePlanTaskBuilder
    {
        public static FinalFinancePlanTask Build(Milestones milestones)
        {
            if (milestones == null)
                return new FinalFinancePlanTask();

            return new FinalFinancePlanTask()
            {
                ExpectedDateGrade6WillSignOffTheFinalPlan = milestones.FsgPreOpeningMilestonesFpaForecastDate,
                ConfirmedTrustHasProvidedFinalPlan = milestones.FsgPreOpeningMilestonesFfpConfirmedTrustHasProvidedFinalPlan,
                Grade6SignedOffFinalPlanDate = milestones.FsgPreOpeningMilestonesFpaActualDateOfCompletion,
                SentFinalPlanToRevenueFundingMailbox = milestones.FsgPreOpeningMilestonesFfpSentFinalPlanToRevenueFundingMailbox,
                SavedFinalPlanInWorkplacesFolder = milestones.FsgPreOpeningMilestonesFfpSavedFinalPlanInWorkplacesFolder
            };
        }

    }
}
