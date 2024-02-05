using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;


public interface ICreateTasksService
{
    Task Execute(string projectId);
}

public class CreateTasksService : ICreateTasksService
{
    private readonly MfspContext _context; 
    
    public CreateTasksService(MfspContext context)
    {
        _context = context;
    }

    public async Task Execute(string projectId)
    {
        var kpi = await _context.Kpi.SingleOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);
        await _context.Tasks.AddRangeAsync(CreateTasks(kpi.Rid));
        await _context.SaveChangesAsync();
    }
    
    private IEnumerable<Data.Entities.Existing.Tasks> CreateTasks(string kpiRid)
    {
        const Status status = Status.NotStarted;

        return new List<Data.Entities.Existing.Tasks>
        {
            new() { Rid = kpiRid, TaskName = TaskName.School, Status = status },
            new() { Rid = kpiRid, TaskName = TaskName.Dates, Status = status },
            new() { Rid = kpiRid, TaskName = TaskName.Trust, Status = status},
            new() { Rid = kpiRid, TaskName = TaskName.RegionAndLocalAuthority, Status = status },
            new() { Rid = kpiRid, TaskName = TaskName.RiskAppraisalMeeting, Status = status },
            new() { Rid = kpiRid, TaskName = TaskName.KickOffMeeting, Status = status },
            new() { Rid = kpiRid, TaskName = TaskName.ArticlesOfAssociation, Status = status },
        };
    }
}