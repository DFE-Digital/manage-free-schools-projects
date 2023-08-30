namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public class ProjectTaskListSummaryResponse
    {
        public TaskSummaryCollectionResponse Tasks { get; set; }
    }

    public class TaskSummaryCollectionResponse
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
