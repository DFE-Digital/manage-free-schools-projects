using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementSubmission
{
    public record FundingAgreementSubmissionTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class FundingAgreementSubmissionTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(FundingAgreementSubmissionTaskSummaryBuilderParameters parameters)
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
