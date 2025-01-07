using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Migrations;
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

        string projectStatusReason = 
       

        dbProject.ProjectStatusProjectStatus = ProjectMapper.FromProjectStatusType(request.ProjectStatus);
        dbProject.ProjectStatusDateClosed = request.ClosedDate;

        dbProject.ProjectStatusDateCancelled = request.CancelledDate;
        dbProject.ProjectStatusPrimaryReasonForCancellation = ProjectMapper.FromProjectCancelledReasonType(request.ProjectCancelledReason);
        dbProject.ProjectStatusProjectCancelledDueToNationalReviewOfPipelineProjects = request.ProjectCancelledDueToNationalReviewOfPipelineProjects;
        dbProject.ProjectStatusCommentaryForCancellation = request.CommentaryForCancellation;

        dbProject.ProjectStatusDateWithdrawn = request.WithdrawnDate;
        dbProject.ProjectStatusPrimaryReasonForWithdrawal = ProjectMapper.FromProjectWithdrawnReasonType(request.ProjectWithdrawnReason);
        dbProject.ProjectStatusProjectWithdrawnDueToNationalReviewOfPipelineProjects = request.ProjectWithdrawnDueToNationalReviewOfPipelineProjects;
        dbProject.ProjectStatusCommentaryForWithdrawal = request.CommentaryForWithdrawal;

        await _context.SaveChangesAsync();
    }
    
}