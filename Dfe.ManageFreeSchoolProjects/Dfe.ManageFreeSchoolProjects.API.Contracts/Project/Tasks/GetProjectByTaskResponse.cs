namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class GetProjectByTaskResponse
    {
        public RiskAppraisalTask RiskAppraisal { get; set; }
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public ConstructionTask Construction { get; set; }
    }
}
