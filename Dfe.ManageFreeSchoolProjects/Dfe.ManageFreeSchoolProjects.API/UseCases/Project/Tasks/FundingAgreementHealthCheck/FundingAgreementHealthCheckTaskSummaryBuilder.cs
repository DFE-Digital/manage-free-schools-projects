using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementHealthCheck
{
    public record FundingAgreementHealthCheckTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class FundingAgreementHealthCheckTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(FundingAgreementHealthCheckTaskSummaryBuilderParameters parameters)
        {
            var taskSummary = parameters.TaskSummary;
            var applicationWave = parameters.ApplicationWave;

            if (applicationWave is not "FS - Presumption")
            {
                taskSummary.IsHidden = true;
            }
             
            return taskSummary;
        }
    }
}
