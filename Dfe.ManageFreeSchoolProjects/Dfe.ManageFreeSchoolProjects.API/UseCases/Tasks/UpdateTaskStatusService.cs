using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public interface IUpdateTaskStatusService
{
    Task Execute(string projectId, ProjectTaskStatus projectTaskStatus);
}

public class UpdateTaskStatusService : IUpdateTaskStatusService
{
    private readonly MfspContext _context;

    public UpdateTaskStatusService(MfspContext context)
    {
        _context = context;
    }

    public async Task Execute(string projectId, ProjectTaskStatus projectTaskStatus) //might need this later to form Composite key: string taskId
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(e => e.Rid == projectId);

        task.Status = (Status) Enum.Parse(typeof(Status), projectTaskStatus.ToString());

        await _context.SaveChangesAsync();
    }
}