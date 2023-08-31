namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class ProjectTaskListSummaryResponse
    {
        public TaskSummaryResponse School { get; set; }

        public TaskSummaryResponse Construction { get; set; }
    }

    public class TaskSummaryResponse
    {
        public string Name { get; set; }
        public ProjectTaskStatus Status { get; set; }
    }
}
