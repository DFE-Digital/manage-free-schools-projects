using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipleDesignate
{
    public record PrincipleDesignateTaskSummaryBuilderParameters
    {
        public string ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }

    public class PrincipleDesignateTaskSummaryBuilder
    {
        public TaskSummaryResponse Build(PrincipleDesignateTaskSummaryBuilderParameters parameters)
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
