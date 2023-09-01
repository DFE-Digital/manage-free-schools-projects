namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{

    public record DatesTask
    {
        public string DateOfEntryIntoPreopening { get; set; }
        public string RealisticYearOfOpening { get; set; }
        public string ProvisionalOpeningDateAgreedWithTrust { get; set; }
        public string ActualOpeningDate { get; set; }
        public string OpeningAcademicYear { get; set; }
        public string StartOfTermDate { get; set; }
        public string ProvisionalKickoffMeetingDate { get; set; }
        public string ActualKickOffMeetingDate { get; set; }
    }
}
