using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.ProjectStatus;

public interface IUpdateProjectStatusService
{
    Task Execute(string projectId , UpdateProjectStatusRequest request);
}

public class UpdateProjectStatusService : IUpdateProjectStatusService
{
    private readonly MfspContext _context;

    public UpdateProjectStatusService(MfspContext context)
    {
        _context = context;
    }

    public async Task Execute(string projectId, UpdateProjectStatusRequest request)
    {
        var dbProject = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        string projectStatusReason = ProjectMapper.FromProjectStatusReasonType(request.ProjectStatusReason);
        string projectStatusCancelledReason = request.ProjectStatus == Contracts.Project.ProjectStatus.Cancelled ? projectStatusReason : "";
        string projectStatusWithdrawnReason = request.ProjectStatus == Contracts.Project.ProjectStatus.WithdrawnInPreOpening ? projectStatusReason : "";

        dbProject.ProjectStatusProjectStatus = ProjectMapper.FromProjectStatusType(request.ProjectStatus);
        dbProject.ProjectStatusDateClosed = request.ClosedDate;
        dbProject.ProjectStatusDateCancelled = request.CancelledDate;
        dbProject.ProjectStatusDateWithdrawn = request.WithdrawnDate;
        dbProject.ProjectStatusPrimaryReasonForCancellation = projectStatusCancelledReason;
        dbProject.ProjectStatusPrimaryReasonForWithdrawal = projectStatusWithdrawnReason;

        await _context.SaveChangesAsync();
    }
    
}