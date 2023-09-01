namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record RiskAppraisalTask
    {
        public string SharepointLink { get; set; }
        public string EducationRiskRating { get; set; }
        public string GovernanceRiskRating { get; set; }
        public string FinanceRiskRating { get; set; }
        public bool MarkedAsComplete { get; set; }
    }
}
