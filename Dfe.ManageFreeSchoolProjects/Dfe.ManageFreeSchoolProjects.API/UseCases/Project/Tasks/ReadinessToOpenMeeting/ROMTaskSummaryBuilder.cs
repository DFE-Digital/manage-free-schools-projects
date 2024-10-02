using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReadinessToOpenMeeting
{
    public record ROMTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class ROMTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(ROMTaskSummaryBuilderParameters parameters)
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
