using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipalDesignate
{
    public record PrincipalDesignateTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class PrincipalDesignateTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(PrincipalDesignateTaskSummaryBuilderParameters parameters)
        {
            var taskSummary = parameters.TaskSummary;
            var applicationWave = parameters.ApplicationWave;

            return taskSummary;
        }
    }
}
