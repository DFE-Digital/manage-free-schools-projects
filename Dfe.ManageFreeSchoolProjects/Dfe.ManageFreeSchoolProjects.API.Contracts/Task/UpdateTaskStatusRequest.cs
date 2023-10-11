using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Task;

public class UpdateTaskStatusRequest
{
    public string TaskName { get; set; }

    public ProjectTaskStatus ProjectTaskStatus { get; set; }
}