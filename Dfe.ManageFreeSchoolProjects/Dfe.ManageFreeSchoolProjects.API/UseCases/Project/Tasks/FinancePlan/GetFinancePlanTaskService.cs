using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan
{
    public class GetFinancePlanTaskService : IGetTaskService
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
                                join po in _context.Po on kpi.Rid equals po.Rid into joinedPo
                                from po in joinedPo.DefaultIfEmpty()
                                select new GetProjectByTaskResponse()
                                {
                                    FinancePlan = FinancePlanTaskBuilder.Build(milestones, po)
                                }).FirstOrDefaultAsync();

            return result ?? new GetProjectByTaskResponse() { FinancePlan = new() };
        }
    }
}
