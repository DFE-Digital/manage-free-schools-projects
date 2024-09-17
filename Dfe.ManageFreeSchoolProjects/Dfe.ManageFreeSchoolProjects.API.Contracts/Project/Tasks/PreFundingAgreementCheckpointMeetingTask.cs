namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class PreFundingAgreementCheckpointMeetingTask
{
    public string TypeOfMeetingHeld { get; set; }
    public string WhyAMeetingWasNotHeld { get; set; }
    public DateTime? DateOfTheMeeting { get; set; }
    public bool? CommissionedExternalExpert { get; set; }
    public bool? SavedMeetingNoteInWorkplacesFolder { get; set; }
    public bool? SentAnEmailToTheTrust { get; set; }
}