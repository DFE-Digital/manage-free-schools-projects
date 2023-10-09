using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public class GetTasksService
{
    private readonly MfspContext _context;

    public GetTasksService(MfspContext context)
    {
        _context = context;
    }

    public async Task<List<TaskSummaryResponse>> Execute(string projectId)
    {
        var dbTasks = await _context.Tasks.Where(x => x.Rid == projectId).ToListAsync();

        var response = dbTasks.Select(task => new TaskSummaryResponse
        {
            Name = task.TaskName.ToString(),
            Status = MapTaskStatus(task.Status)
        });

        return response.ToList();
    }

    private ProjectTaskStatus MapTaskStatus(Status taskStatus) => taskStatus switch
    {
        Status.InProgress => ProjectTaskStatus.InProgress,
        Status.NotStarted => ProjectTaskStatus.NotStarted,
        Status.Completed => ProjectTaskStatus.Completed,
        _ => throw new ArgumentOutOfRangeException(nameof(taskStatus), taskStatus, null)
    };
}