using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Section10Consultation;

public class ViewSection10ConsultationTask : ViewTaskBaseModel
{
    private readonly ILogger<ViewSection10ConsultationTask> _logger;

    public ViewSection10ConsultationTask(
        IGetProjectByTaskService getProjectService,
        ILogger<ViewSection10ConsultationTask> logger,
        IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService) : base(getProjectService, getTaskStatusService, updateTaskStatusService)
    {
        _logger = logger;
    }

    public async Task<ActionResult> OnGet()
    {
        _logger.LogMethodEntered();

        await GetTask(TaskName.Section10Consultation);

        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        _logger.LogMethodEntered();

        await PostTask(TaskName.Section10Consultation);

        return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
    }
}

