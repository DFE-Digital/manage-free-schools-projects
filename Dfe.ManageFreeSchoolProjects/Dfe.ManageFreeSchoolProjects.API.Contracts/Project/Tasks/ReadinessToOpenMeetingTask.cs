using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class ReadinessToOpenMeetingTask
{
    public YesNo? AROMIsExpectedToHappen { get; set; }

    public DateTime? ExpectedDateOfTheMeeting { get; set; }

    public TypeOfMeetingHeld TypeOfMeetingHeld { get; set; }
    
    public DateTime? DateOfTheMeeting { get; set; }
    
    public string WhyMeetingWasNotHeld { get; set; }

    public bool? PrincipalDesignateHasProvidedTheChecklist { get; set; }

    public bool? CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable { get; set; }
    
    public bool? SavedTheInternalRomReportToWorkplacesFolder { get; set; }
    
    public bool? SavedTheExternalRomReportToWorkplacesFolder { get; set; }
}