namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;

    public record ContactsTask
    {
        public Contact ProjectAssignedTo { get; set; } = new();

        public Contact TeamLead { get; set; } = new();

        public Contact Grade6 { get; set; } = new();

        public Contact ProjectManager { get; set; } = new();

        public Contact ProjectDirector { get; set; } = new();

        public Contact TrustContact { get; set; } = new();

        public Contact SchoolChair { get; set; } = new();

}

