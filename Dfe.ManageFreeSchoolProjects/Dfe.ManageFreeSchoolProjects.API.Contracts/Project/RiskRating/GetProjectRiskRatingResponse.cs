namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating
{
    public class GetProjectRiskRatingResponse
    {
        public ProjectRiskRatingEntryResponse GovernanceAndSuitabilityRisk { get; set; }

        public ProjectRiskRatingEntryResponse EducationRisk { get; set; }

        public ProjectRiskRatingEntryResponse FinanceRisk { get; set; }

        public ProjectRiskRatingEntryResponse OverallRisk { get; set; }
        
        public string RiskAppraisalFormSharepointLink { get; set; }
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
