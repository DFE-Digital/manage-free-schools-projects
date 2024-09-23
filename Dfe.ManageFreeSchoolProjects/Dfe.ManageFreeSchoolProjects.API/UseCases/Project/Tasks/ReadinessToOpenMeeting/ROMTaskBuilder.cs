using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReadinessToOpenMeeting;

public static class ROMTaskBuilder
{
    public static ReadinessToOpenMeetingTask Build(Milestones milestones)
    {
        if (milestones == null)
            return new ReadinessToOpenMeetingTask();

        return new ReadinessToOpenMeetingTask
        {
            DateOfTheMeeting = milestones.FsgPreOpeningMilestonesRomActualDateOfCompletion,
            TypeOfMeetingHeld = EnumParsers.ParseTypeOfMeetingHeld(milestones.ROMTypeOfMeetingHeld),
            WhyMeetingWasNotHeld = milestones.ROMWhyAMeetingWasNotHeld,
            PrincipalDesignateHasProvidedTheChecklist = milestones.ROMPrincipalDesignateHasProvidedChecklist,
            SavedTheInternalRomReportToWorkplacesFolder = milestones.ROMSavedTheInternalROMReportInWorkplacesFolder,
            SavedTheExternalRomReportToWorkplacesFolder = milestones.ROMSavedTheExternalROMReportInWorkplacesFolder,
            CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable =
                milestones.ROMCommissionedAnExternalExpertToAttendMeetingsIfApplicable
        };
    }
}