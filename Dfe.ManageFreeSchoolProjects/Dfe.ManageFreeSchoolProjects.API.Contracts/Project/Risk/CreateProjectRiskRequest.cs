namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk
{
    public class CreateProjectRiskRequest
    {
        public ProjectRiskEntryRequest GovernanceAndSuitability { get; set; }

        public ProjectRiskEntryRequest Education { get; set; }

        public ProjectRiskEntryRequest Finance { get; set; }

        public ProjectRiskEntryRequest Overall { get; set; }

        public string RiskAppraisalFormSharepointLink { get; set; }
    }

    public class ProjectRiskEntryRequest
    {
        public ProjectRiskRating RiskRating { get; set; }
        public string Summary { get; set; }
    }
}
