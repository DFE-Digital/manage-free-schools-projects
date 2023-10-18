namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record SchoolTask
    {
        public string CurrentFreeSchoolName { get; set; }
        public string SchoolType { get; set; }
        public string SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public string Nursery { get; set; }
        public string SixthForm { get; set; }
        public string CompanyName { get; set; }
        public string NumberOfCompanyMembers { get; set; }
        public string ProposedChairOfTrustees { get; set; }
        
        public bool MarkedAsComplete { get; set; }
    }
}
