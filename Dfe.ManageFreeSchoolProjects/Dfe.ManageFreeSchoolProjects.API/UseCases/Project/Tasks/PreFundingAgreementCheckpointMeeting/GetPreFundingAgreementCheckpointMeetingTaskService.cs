using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public class GetPreFundingAgreementCheckpointMeetingTaskService(MfspContext context) : IGetTaskService
    {

        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var result = await (from kpi in parameters.BaseQuery
                                join milestones in context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
                                from milestones in joinedMilestones.DefaultIfEmpty()
                                select new GetProjectByTaskResponse
                                {
                                    PreFundingAgreementCheckpointMeetingTask = PreFundingAgreementCheckpointMeetingTaskBuilder.Build(milestones)
                                }).FirstOrDefaultAsync();

            var preFundingAgreementCheckpointMeeting = result.PreFundingAgreementCheckpointMeetingTask;

            if (preFundingAgreementCheckpointMeeting.DateOfTheMeeting.HasValue && preFundingAgreementCheckpointMeeting.TypeOfMeetingHeld == TypeOfMeetingHeld.NotSet)
            {
                preFundingAgreementCheckpointMeeting.TypeOfMeetingHeld = TypeOfMeetingHeld.FormalCheckpointMeeting;
            }

            return result;
        }
    }
}