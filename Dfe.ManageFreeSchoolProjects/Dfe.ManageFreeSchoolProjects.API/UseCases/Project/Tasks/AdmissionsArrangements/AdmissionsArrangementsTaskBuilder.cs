using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.AdmissionsArrangements
{
    public static class AdmissionsArrangementsTaskBuilder
    {
        public static AdmissionsArrangementsTask Build(Milestones milestones)
        {
            if (milestones == null)
            {
                return new AdmissionsArrangementsTask();
            }

            return new AdmissionsArrangementsTask()
            {
                ExpectedDateThatTrustWillConfirmArrangements = milestones.FsgPreOpeningMilestonesSapForecastDate,
                TrustConfirmedAdmissionsArrangementsTemplate = milestones.FsgPreOpeningMilestonesAdmissionsArrangementsRecommendedTemplate,
                TrustConfirmedAdmissionsArrangementsPolicies = milestones.FsgPreOpeningMilestonesAdmissionsArrangementsComplyWithPolicies,
                ActualDateThatTrustConfirmedArrangements = milestones.FsgPreOpeningMilestonesSapActualDateOfCompletion,
                SavedToWorkplaces = milestones.FsgPreOpeningMilestonesAdmissionsArrangementsSavedToWorkplaces
            };

        }
    }
}
