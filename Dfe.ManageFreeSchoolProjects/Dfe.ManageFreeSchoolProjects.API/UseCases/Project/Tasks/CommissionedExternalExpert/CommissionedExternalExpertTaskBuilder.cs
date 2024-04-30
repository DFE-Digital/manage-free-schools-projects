using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.CommissionedExternalExpert
{
    public static class CommissionedExternalExpertTaskBuilder
    {
        public static CommissionedExternalExpertTask Build(Milestones milestones)
        {
            if (milestones == null)
            {
                return new CommissionedExternalExpertTask();
            }

            return new CommissionedExternalExpertTask()
            {
                CommissionedExternalExpertVisit =
                    milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisit,
                ExternalExpertVisitDate = 
                    milestones.FsgPreOpeningMilestonesExternalExpertVisitDate,
                SavedExternalExpertSpecsToWorkplacesFolder = milestones.FsgPreOpeningMilestoneSavedExternalExpertSpecsToWorkplacesFolder,
            };

        }
    }
}
