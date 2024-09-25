using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public static class PreFundingAgreementCheckpointMeetingTaskBuilder
    {
        public static PreFundingAgreementCheckpointMeetingTask Build(Milestones milestones)
        {
            if (milestones == null)
                return new PreFundingAgreementCheckpointMeetingTask();

            return new PreFundingAgreementCheckpointMeetingTask
            {
                DateOfTheMeeting = milestones.FsgPreOpeningMilestonesPfacmActualDateOfCompletion,
                TypeOfMeetingHeld = EnumParsers.ParseTypeOfMeetingHeld(milestones.PFACMTypeOfMeetingHeld),
                WhyMeetingWasNotHeld = milestones.PFACMWhyAMeetingWasNotHeld,
                CommissionedExternalExpert = milestones.PFACMCommissionedAnExternalExpertToAttendMeetingsIfApplicable,
                SavedMeetingNoteInWorkplacesFolder = milestones.PFACMSavedMeetingNoteInWorkplacesFolder,
                SentAnEmailToTheTrust = milestones.PFACMSentAnEmailToTheTrust
            }; 
        }
    }
}
