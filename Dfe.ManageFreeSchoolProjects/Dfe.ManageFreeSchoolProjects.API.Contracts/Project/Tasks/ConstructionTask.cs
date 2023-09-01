namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record ConstructionTask
    {
        public string NameOfSite { get; set; }
        public string AddressOfSite { get; set; }
        public string PostcodeOfSite { get; set; }
        public string BuildingType { get; set; }
        public string TrustRef { get; set; }
        public string TrustLeadSponsor { get; set; }
        public string TrustName { get; set; }
        public string SiteMinArea { get; set; }
        public string TypeofWorksLocation { get; set; }
    }
}
