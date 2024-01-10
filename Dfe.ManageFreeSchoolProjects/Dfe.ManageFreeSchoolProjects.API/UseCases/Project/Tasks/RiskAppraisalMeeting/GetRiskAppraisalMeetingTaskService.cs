using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RiskAppraisalMeeting
{
    public class GetRiskAppraisalMeetingTaskService : IGetTaskService
    {
        private readonly MfspContext _context;

        public GetRiskAppraisalMeetingTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var query = parameters.BaseQuery;

            var result = await (from kpi in query
                                join riskAppraisalMeetingTask in _context.RiskAppraisalMeetingTask on kpi.Rid equals riskAppraisalMeetingTask.RID into riskAppraisalMeetingTaskJoin
                                from riskAppraisalMeetingTask in riskAppraisalMeetingTaskJoin.DefaultIfEmpty()
                                select new GetProjectByTaskResponse()
                                {
                                    RiskAppraisalMeeting = Map(riskAppraisalMeetingTask)
                                }).FirstOrDefaultAsync();

            return result;
        }

        private static RiskAppraisalMeetingTask Map(Data.Entities.RiskAppraisalMeetingTask riskAppraisalMeetingTask)
        {
            if (riskAppraisalMeetingTask == null)
            {
                return new RiskAppraisalMeetingTask();
            }

            return new RiskAppraisalMeetingTask
            {
                InitialRiskAppraisalMeetingCompleted = riskAppraisalMeetingTask.MeetingCompleted,
                ForecastDate = riskAppraisalMeetingTask.ForecastDate,
                ActualDate = riskAppraisalMeetingTask.ActualDate,
                CommentsOnDecisionToApprove = riskAppraisalMeetingTask.CommentOnDecision,
                ReasonNotApplicable = riskAppraisalMeetingTask.ReasonNotApplicable
            };
        }
    }
}
