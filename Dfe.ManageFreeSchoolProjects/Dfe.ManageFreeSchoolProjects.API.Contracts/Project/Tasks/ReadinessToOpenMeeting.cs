namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class ReadinessToOpenMeeting
{
    public TypeOfMeetingHeld TypeOfMeetingHeld { get; set; }
    
    public DateTime? DateOfTheMeeting { get; set; }
    
    public string WhyMeetingWasNotHeld { get; set; }

    public bool? PrincipalDesignateHasProvidedTheChecklist { get; set; }

    public bool? CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable { get; set; }
    
    public bool? SavedTheInternalRomReportToWorkplacesFolder { get; set; }
    
    public bool? SavedTheExternalRomReportToWorkplacesFolder { get; set; }
}
public enum TypeOfMeetingHeld
{
    FormalMeeting, 
    InformalMeeting, 
    NoRomHeld
}