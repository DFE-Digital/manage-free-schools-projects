namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{

    public record DatesTask
    {
        public DateTime? DateOfEntryIntoPreopening { get; set; }
        public DateTime? ProvisionalOpeningDateAgreedWithTrust { get; set; }
        
        public DateTime? ProjectClosedDate { get; set; }
        
        public DateTime? ProjectCancelledDate { get; set; }
        
        public DateTime? ProjectWithdrawnDate { get; set; }
    }
}
