using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.MovingToOpen
{
    public static class MovingToOpenTaskBuilder
    {
        public static MovingToOpenTask Build(Kpi kpi,Milestones milestones)
        {
            if (milestones == null)
            {
                return new MovingToOpenTask();
            }

            return new MovingToOpenTask()
            {
               
                ProjectBriefToSfso = milestones.FsgPreOpeningMilestoneMovingToOpenProjectBriefToSfso,
                ProjectBriefToEducationEstates = milestones.FsgPreOpeningMilestoneMovingToOpenProjectBriefToEducationEstates,
                ProjectBriefToNewDeliveryOfficer = milestones.FsgPreOpeningMilestoneMovingToOpenProjectBriefToNewDeliveryOfficer,
                SentEmailsToRelevantContacts = milestones.FsgPreOpeningMilestoneMovingToOpenSentEmailsToRelevantContacts,
                SentEmailsToSchoolsPrinciple = milestones.FsgPreOpeningMilestoneMovingToOpenSentEmailsToSchoolsPrinciple,
                SavedToWorkplacesFolderProjectBrief = milestones.FsgPreOpeningMilestoneMovingToOpenSavedToWorkplacesFolderProjectBrief,
                SavedToWorkplacesFolderAnnexB = milestones.FsgPreOpeningMilestoneMovingToOpenSavedToWorkplacesFolderAnnexB,
                SavedToWorkplacesFolderAnnexE = milestones.FsgPreOpeningMilestoneMovingToOpenSavedToWorkplacesFolderAnnexE,
                ActualOpeningDate = kpi.ProjectStatusActualOpeningDate
            };

        }
    }
}
