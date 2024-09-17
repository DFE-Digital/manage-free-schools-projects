using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public static class PreFundingAgreementCheckpointMeetingTaskBuilder
    {
        public static PreFundingAgreementCheckpointMeetingTask Build(Milestones milestones)
        {
            return new PreFundingAgreementCheckpointMeetingTask()
            {
                TypeOfMeetingHeld = null,
                WhyAMeetingWasNotHeld = null,
                DateOfTheMeeting = null,
                CommissionedExternalExpert = null,
                SavedMeetingNoteInWorkplacesFolder = null,
                SentAnEmailToTheTrust = null
            };
        }
    }
}
