using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.FinalFinancePlan
{
    public class ViewFinalFinancePlanTaskModel(
        IGetProjectByTaskService getProjectService,
        ILogger<ViewFinalFinancePlanTaskModel> logger,
        IGetTaskStatusService getTaskStatusService,
        IUpdateTaskStatusService updateTaskStatusService)
        : ViewTaskBaseModel(getProjectService, getTaskStatusService, updateTaskStatusService)
    {
        public async Task<ActionResult> OnGet()
        {
            logger.LogMethodEntered();

            await GetTask(TaskName.FinalFinancePlan);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            logger.LogMethodEntered();

            await PostTask(TaskName.FinalFinancePlan);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
