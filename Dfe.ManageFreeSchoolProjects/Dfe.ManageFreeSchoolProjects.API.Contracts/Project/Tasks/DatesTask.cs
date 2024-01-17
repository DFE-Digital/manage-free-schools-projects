namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{

    public record DatesTask
    {
        public DateTime? DateOfEntryIntoPreopening { get; set; }
        public string OpeningAcademicYear { get; set; }
        public DateTime? ProvisionalOpeningDateAgreedWithTrust { get; set; }
    }
}
