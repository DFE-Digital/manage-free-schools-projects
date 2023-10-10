using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public static class TaskStatusMapper
{
    public static ProjectTaskStatus MapTaskStatus(Status taskStatus) => taskStatus switch
    {
        Status.NotStarted => ProjectTaskStatus.NotStarted,
        Status.InProgress => ProjectTaskStatus.InProgress,
        Status.Completed => ProjectTaskStatus.Completed,
        _ => throw new ArgumentOutOfRangeException(nameof(taskStatus), taskStatus, null)
    };
    
    public static Status MapProjectTaskStatus(ProjectTaskStatus projectTaskStatus) => projectTaskStatus switch
    {
        ProjectTaskStatus.NotStarted => Status.NotStarted,
        ProjectTaskStatus.InProgress => Status.InProgress,
        ProjectTaskStatus.Completed => Status.Completed,
        _ => throw new ArgumentOutOfRangeException(nameof(projectTaskStatus), projectTaskStatus, null)
    };
}