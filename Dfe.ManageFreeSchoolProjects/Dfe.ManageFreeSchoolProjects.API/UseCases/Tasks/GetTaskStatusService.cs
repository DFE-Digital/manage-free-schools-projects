﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public interface IGetTaskStatusService
{
    Task<ProjectTaskStatus> Execute(string projectId, string taskName);
}

public class GetTaskStatusService : IGetTaskStatusService
{
    private readonly MfspContext _context;

    public GetTaskStatusService (MfspContext context)
    {
        _context = context;
    }

    public async Task<ProjectTaskStatus> Execute(string projectId, string taskName)
    {
        var dbKpi = await _context.Kpi.SingleOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);
        var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Rid == dbKpi.Rid && x.TaskName == Enum.Parse<TaskName>(taskName));
        var mappedStatus = TaskStatusMapper.MapTaskStatus(task.Status); 
        
        return mappedStatus;
    }
}

