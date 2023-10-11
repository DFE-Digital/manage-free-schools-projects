namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class UpdateProjectByTaskRequest
    {
        public RiskAppraisalTask RiskAppraisal { get; set; }
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public ConstructionTask Construction { get; set; }

        public string TaskNameToUpdate
        {
            get
            {
                if (School != null)
                    return "School";
                if (Dates != null)
                    return "Dates";
                if (Construction != null)
                    return "Construction";
                if (RiskAppraisal != null)
                    return "RiskAppraisal";
                return null;
            }
        }
    }
}
