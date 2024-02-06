using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan
{
    public class GetFinancePlanTaskService
    {
        private readonly MfspContext _context;

        public GetFinancePlanTaskService(MfspContext context)
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
                                    FinancePlan = new()
                                    {
                                        FinancePlanAgreed = ConvertYesNo(milestones.FsgPreOpeningMilestonesBefpApplicable),
                                        DateAgreed = milestones.FsgPreOpeningMilestonesBefpActualDateOfCompletion,
                                        PlanSavedInWorkspaceFolder = milestones.IsPlanSavedInWorkspaceFolder,
                                        LocalAuthorityAgreedPupilNumbers = milestones.LAAgreedPupilNumbers,
                                        Comments = milestones.FsgPreOpeningMilestonesMi72CommentsOnDecisionToApproveIfApplicable,
                                        TrustWillOptIntoRpa = milestones.TrustOptInRPA
                                    }
                                }).FirstOrDefaultAsync();

            return result ?? new GetProjectByTaskResponse() { FinancePlan = new() };
        }

        private static YesNo? ConvertYesNo(string value)
        {
            return Enum.TryParse<YesNo>(value, true, out var result) ? result : null;
        }
    }
}
