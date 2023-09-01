namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class UpdateProjectByTaskRequest
    {
        public RiskAppraisalTask RiskAppraisal { get; set; }
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public ConstructionTask Construction { get; set; }
    }
}
