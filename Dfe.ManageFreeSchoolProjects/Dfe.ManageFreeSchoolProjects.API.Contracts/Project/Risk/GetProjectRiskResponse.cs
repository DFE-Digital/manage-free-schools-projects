namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk
{
    public class GetProjectRiskResponse
    {
        public DateTime Date { get; set; }

        public ProjectRiskEntryResponse GovernanceAndSuitability { get; set; }

        public ProjectRiskEntryResponse Education { get; set; }

        public ProjectRiskEntryResponse Finance { get; set; }

        public ProjectRiskEntryResponse Overall { get; set; }

        public string RiskAppraisalFormSharepointLink { get; set; }

        public List<ProjectRiskHistoryResponse> History { get; set; }
    }

    public class ProjectRiskHistoryResponse
    {
        public DateTime Date { get; set; }
        public ProjectRiskRating RiskRating { get; set; }
    }

    public class ProjectRiskEntryResponse
    {
        public ProjectRiskRating RiskRating { get; set; }
        public string Summary { get; set; }
    }

    public enum ProjectRiskRating
    {
        Green = 1,
        AmberGreen = 2,
        AmberRed = 3,
        Red = 4
    }
}
