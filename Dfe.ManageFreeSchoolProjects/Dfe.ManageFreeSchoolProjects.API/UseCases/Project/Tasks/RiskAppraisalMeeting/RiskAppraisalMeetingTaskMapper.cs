using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RiskAppraisalMeeting
{
    public static class RiskAppraisalMeetingTaskMapper
    {
        public static RiskAppraisalMeetingTask Map(Data.Entities.RiskAppraisalMeetingTask riskAppraisalMeetingTask)
        {
            if (riskAppraisalMeetingTask == null)
            {
                return null;
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
