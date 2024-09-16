using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReadinessToOpenMeeting;

public class GetReadinessToOpenMeetingService(MfspContext context) : IGetTaskService
{
    public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
    {
        return await (from kpi in parameters.BaseQuery
            join milestones in context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
            from milestones in joinedMilestones.DefaultIfEmpty()
            select new GetProjectByTaskResponse
            {
                ReadinessToOpenMeetingTask = ROMTaskBuilder.Build(milestones)
            }).FirstOrDefaultAsync();
    }
}