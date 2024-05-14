using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.MovingToOpen;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.AdmissionsArrangements
{
    public class GetMovingToOpenTaskService : IGetTaskService
    {
        private readonly MfspContext _context;

        public GetMovingToOpenTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var result = await(from kpi in parameters.BaseQuery
                join milestones in _context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
                from milestones in joinedMilestones.DefaultIfEmpty()
                select new GetProjectByTaskResponse()
                {
                    MovingToOpen= MovingToOpenTaskBuilder.Build(kpi,milestones)
                }).FirstOrDefaultAsync();

            return result ?? new GetProjectByTaskResponse() { MovingToOpen = new () };
        }
    }
}
