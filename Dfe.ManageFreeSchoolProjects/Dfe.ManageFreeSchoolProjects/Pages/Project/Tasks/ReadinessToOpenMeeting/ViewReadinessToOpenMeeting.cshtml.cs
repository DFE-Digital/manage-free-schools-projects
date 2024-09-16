using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.ReadinessToOpenMeeting;

public class ViewReadinessToOpenMeeting(
    IGetProjectByTaskService getProjectService,
    IGetTaskStatusService getTaskStatusService,
    IUpdateTaskStatusService updateTaskStatusService,
    ILogger<ViewReadinessToOpenMeeting> logger)
    : ViewTaskBaseModel(getProjectService, getTaskStatusService, updateTaskStatusService)
{
    public async Task<ActionResult> OnGet()
    {
        logger.LogMethodEntered();

        await GetTask(TaskName.ReadinessToOpenMeeting);

        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        logger.LogMethodEntered();

        await PostTask(TaskName.ReadinessToOpenMeeting);

        return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
    }
}