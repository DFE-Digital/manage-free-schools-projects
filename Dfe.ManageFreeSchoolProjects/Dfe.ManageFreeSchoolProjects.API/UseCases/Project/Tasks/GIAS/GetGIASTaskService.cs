using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.GIAS;

internal class GetGIASTaskService : IGetTaskService
{
    private readonly MfspContext _context;

    public GetGIASTaskService(MfspContext context)
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
                KickOffMeeting = new()
                {
                    FundingArrangementAgreed =
                        milestones.FsgPreOpeningMilestonesFundingArrangementAgreedBetweenLaAndSponsor,
                    FundingArrangementDetailsAgreed = milestones
                        .FsgPreOpeningMilestonesDetailsOfFundingArrangementAgreedBetweenLaAndSponsor,
                    ProvisionalOpeningDate = kpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust,
                    RealisticYearOfOpening = kpi.ProjectStatusRealisticYearOfOpening,
                    SharepointLink = milestones.FsgPreOpeningMilestonesMi141LinkToSavedDocument
                }
            }).FirstOrDefaultAsync();

        return result ?? new GetProjectByTaskResponse() { KickOffMeeting = new() };
    }

}
    
    