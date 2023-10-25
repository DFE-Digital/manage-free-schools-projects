namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{

    public record TrustTask
    {
        public string TRN { get; set; }
        public string TrustName { get; set; }
        public string TrustType { get; set; }
    }
}
