using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DraftGovernancePlan
{
    public class GetDraftGovernancePlanService : IGetTaskService
    {
        private readonly MfspContext _context;

        public GetDraftGovernancePlanService(MfspContext context)
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
                                    DraftGovernancePlan = new()
                                    {
                                        ForecastDate = milestones.FsgPreOpeningMilestonesDgpForecastDate,
                                        ActualDate = milestones.FsgPreOpeningMilestonesDgpActualDateOfCompletion,
                                        // TODO: Get the correct field mappings
                                        CommentsOnDecisionToApprove = milestones.FsgPreOpeningMilestonesMi103CommentsOnDecisionToApproveIfApplicable,
                                        SharepointLink = milestones.FsgPreOpeningMilestonesMi105LinkToSavedDocument
                                    }
                                }).FirstOrDefaultAsync();

            return result ?? new GetProjectByTaskResponse() { FinancePlan = new() };
        }
    }
}
