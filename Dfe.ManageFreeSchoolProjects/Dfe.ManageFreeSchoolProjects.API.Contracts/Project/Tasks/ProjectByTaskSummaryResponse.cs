using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class ProjectByTaskSummaryResponse
    {
        public TaskSummaryResponse School { get; set; }
        
        public TaskSummaryResponse Dates { get; set; }

        public TaskSummaryResponse Trust { get; set; }
        
        public TaskSummaryResponse RegionAndLocalAuthority { get; set; }

        public TaskSummaryResponse RiskAppraisalMeeting { get; set; }
    }

    public class TaskSummaryResponse
    {
        public string Name { get; set; }
        public ProjectTaskStatus Status { get; set; }
    }
}
