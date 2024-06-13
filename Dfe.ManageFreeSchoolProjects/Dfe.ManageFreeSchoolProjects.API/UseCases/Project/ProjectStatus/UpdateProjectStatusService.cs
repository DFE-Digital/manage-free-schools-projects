using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Contacts;
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

        var updateRequest = new UpdateProjectStatusRequest()
        {
            ProjectStatus = request.ProjectStatus,
            WithdrawnDate = request.WithdrawnDate,
            ClosedDate = request.ClosedDate,
            CancelledDate = request.CancelledDate
        };

        dbProject.ProjectStatusProjectStatus = updateRequest.ProjectStatus.ToDescription();
        dbProject.ProjectStatusDateClosed = updateRequest.ClosedDate;
        dbProject.ProjectStatusDateCancelled = updateRequest.CancelledDate;
        dbProject.ProjectStatusDateWithdrawn = updateRequest.WithdrawnDate;
        
        
        await _context.SaveChangesAsync();
    }
    
}