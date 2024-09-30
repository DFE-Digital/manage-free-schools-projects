using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public record PreFundingAgreementCheckpointMeetingTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class PreFundingAgreementCheckpointMeetingTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(PreFundingAgreementCheckpointMeetingTaskSummaryBuilderParameters parameters)
        {
            var taskSummary = parameters.TaskSummary;
            var applicationWave = parameters.ApplicationWave;

            if (applicationWave is "FS - Presumption")
            {
                taskSummary.IsHidden = true;
            }

            return taskSummary;
        }
    }
}
