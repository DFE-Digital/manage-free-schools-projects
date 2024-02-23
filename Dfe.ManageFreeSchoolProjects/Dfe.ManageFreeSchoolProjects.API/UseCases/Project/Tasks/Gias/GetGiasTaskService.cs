using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Gias;

internal class GetGiasTaskService : IGetTaskService
{
    private readonly MfspContext _context;

    public GetGiasTaskService(MfspContext context)
    {
        _context = context;
    }

    public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
    {
        var result = await (from kpi in parameters.BaseQuery
            join milestones in _context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
            from milestones in joinedMilestones.DefaultIfEmpty()
            select new GetProjectByTaskResponse()
            {
                Gias = new GiasTask()
                {
                    CheckedTrustInformation = milestones.FSGPreOpeningMilestonesGIASCheckedTrustInformation,
                    ApplicationFormSent = milestones.FSGPreOpeningMilestonesGIASApplicationFormSent,
                    SavedToWorkspaces = milestones.FSGPreOpeningMilestonesGIASSavedToWorkspaces,
                    UrnSent = milestones.FSGPreOpeningMilestonesGIASURNSent
                }
            }).FirstOrDefaultAsync();

        return result ?? new GetProjectByTaskResponse() { Gias = new() };
    }

}
    
    