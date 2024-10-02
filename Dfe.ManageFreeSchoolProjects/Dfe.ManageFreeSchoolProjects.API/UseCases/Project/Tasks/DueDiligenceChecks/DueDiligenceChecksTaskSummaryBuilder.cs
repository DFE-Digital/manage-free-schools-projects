using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DueDiligenceChecks
{
    public record DueDiligenceChecksTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class DueDiligenceChecksTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(DueDiligenceChecksTaskSummaryBuilderParameters parameters)
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
