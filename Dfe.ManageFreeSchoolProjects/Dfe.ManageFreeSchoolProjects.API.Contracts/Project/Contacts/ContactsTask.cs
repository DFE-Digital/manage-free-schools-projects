namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;

    public record ContactsTask
    {
        public Contact ProjectManagedBy { get; set; } = new();

        public Contact TeamLead { get; set; } = new();

        public Contact Grade6 { get; set; } = new();

        public Contact ProjectDirector { get; set; } = new();

        public Contact TrustChair { get; set; } = new();

        public Contact SchoolChair { get; set; } = new();

}

