namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class PreFundingAgreementCheckpointMeetingTask
{
    public TypeOfMeetingHeld TypeOfMeetingHeld { get; set; }
    public string WhyMeetingWasNotHeld { get; set; }
    public DateTime? DateOfTheMeeting { get; set; }
    public bool? CommissionedExternalExpert { get; set; }
    public bool? SavedMeetingNoteInWorkplacesFolder { get; set; }
    public bool? SentAnEmailToTheTrust { get; set; }
}