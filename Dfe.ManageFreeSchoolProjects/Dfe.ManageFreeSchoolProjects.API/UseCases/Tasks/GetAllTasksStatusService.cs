using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public record GetTasksServiceResult
{
    public string CurrentFreeSchoolName { get; set; }
    public List<TaskSummaryResponse> taskSummaryResponses { get; set; }

}

public interface IGetTasksService
{ 
    Task<GetTasksServiceResult> Execute(string projectId);
}

public class GetAllTasksStatusService : IGetTasksService
{
    private readonly MfspContext _context;

    public GetAllTasksStatusService(MfspContext context)
    {
        _context = context;
    }

    public async Task<GetTasksServiceResult> Execute(string projectId)
    {
        var dbKpi = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        var dbTasks = await _context.Tasks.Where(x => x.Rid == dbKpi.Rid).ToListAsync();
    
        var response = dbTasks.Select(task => new TaskSummaryResponse
        {
            Name = task.TaskName.ToString(),
            Status = task.Status.Map()
        });
    
        return new GetTasksServiceResult() { 
            CurrentFreeSchoolName = dbKpi.ProjectStatusCurrentFreeSchoolName,
            taskSummaryResponses = response.ToList() 
        };
    }
}

