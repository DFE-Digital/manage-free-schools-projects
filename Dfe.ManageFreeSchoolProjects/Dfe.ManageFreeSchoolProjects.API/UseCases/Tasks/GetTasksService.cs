using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public interface IGetTasksService
{ 
    Task<List<TaskSummaryResponse>> Execute(string projectId);
}

public class GetTasksService : IGetTasksService
{
    private readonly MfspContext _context;

    public GetTasksService(MfspContext context)
    {
        _context = context;
    }

    public async Task<List<TaskSummaryResponse>> Execute(string projectId)
    {
        var dbKpi = await _context.Kpi.SingleOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        var dbTasks = await _context.Tasks.Where(x => x.Rid == dbKpi.Rid).ToListAsync();
    
        var response = dbTasks.Select(task => new TaskSummaryResponse
        {
            Name = task.TaskName.ToString(),
            Status = TaskStatusMapper.MapTaskStatus(task.Status)
        });
    
        return response.ToList();
    }
}

