using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PupilNumbersChecks
{
    public class ViewPupilNumbersChecksModel(
        IGetProjectByTaskService getProjectService,
        ILogger<ViewPupilNumbersChecksModel> logger,
        IGetTaskStatusService getTaskStatusService,
        IUpdateTaskStatusService updateTaskStatusService)
        : ViewTaskBaseModel(getProjectService, getTaskStatusService, updateTaskStatusService)
    {
        public async Task<ActionResult> OnGet()
        {
            logger.LogMethodEntered();
            
            await GetTask(TaskName.PupilNumbersChecks);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            logger.LogMethodEntered();
            
            await PostTask(TaskName.PupilNumbersChecks);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
