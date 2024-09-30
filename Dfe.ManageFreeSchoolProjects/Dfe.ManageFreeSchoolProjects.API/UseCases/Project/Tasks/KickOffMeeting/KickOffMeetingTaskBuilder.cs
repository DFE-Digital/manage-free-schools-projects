using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.KickOffMeeting
{
    public static class KickOffMeetingTaskBuilder
    {
        public static KickOffMeetingTask Build(Kpi kpi, Milestones milestones)
        {
            return new KickOffMeetingTask()
            {
                FundingArrangementAgreed = milestones?.FsgPreOpeningMilestonesFundingArrangementAgreedBetweenLaAndSponsor,
                FundingArrangementDetailsAgreed = milestones?.FsgPreOpeningMilestonesDetailsOfFundingArrangementAgreedBetweenLaAndSponsor,
                RealisticYearOfOpening = kpi.ProjectStatusRealisticYearOfOpening,
                SavedDocumentsInWorkplacesFolder = milestones?.FsgPreOpeningMilestonesKickoffMeetingDocumentsSavedInWorkplacesFolder
            };
        }
    }
}
