namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating
{
    public class GetProjectRiskRatingResponse
    {
        public DateTime Date { get; set; }

        public ProjectRiskRatingEntryResponse GovernanceAndSuitability { get; set; }

        public ProjectRiskRatingEntryResponse Education { get; set; }

        public ProjectRiskRatingEntryResponse Finance { get; set; }

        public ProjectRiskRatingEntryResponse Overall { get; set; }
        
        public string RiskAppraisalFormSharepointLink { get; set; }

        public List<ProjectRiskRatingHistoryResponse> History { get; set; }
    }

    public class  ProjectRiskRatingHistoryResponse
    {
        public DateTime Date { get; set; }
        public ProjectRiskRating RiskRating { get; set; }
    }

    public class ProjectRiskRatingEntryResponse
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
