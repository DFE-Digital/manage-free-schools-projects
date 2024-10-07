using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.OfstedPreRegistration
{
    public class ViewOfstedPreRegistrationModel(IGetProjectByTaskService getProjectService, ILogger<ViewOfstedPreRegistrationModel> logger,
        IGetTaskStatusService getTaskStatusService,
        IUpdateTaskStatusService updateTaskStatusService) 
        : ViewTaskBaseModel(getProjectService, getTaskStatusService, updateTaskStatusService)
    {
        public async Task<ActionResult> OnGet()
        {
            logger.LogMethodEntered();

            await GetTask(TaskName.OfstedInspection);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            logger.LogMethodEntered();

            await PostTask(TaskName.OfstedInspection);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
