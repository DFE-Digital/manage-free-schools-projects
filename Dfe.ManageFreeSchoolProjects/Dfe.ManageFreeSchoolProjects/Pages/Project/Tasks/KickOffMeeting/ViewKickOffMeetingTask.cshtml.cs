using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.KickOffMeeting;

public class ViewKickOffMeetingTask : PageModel
{

    private readonly ILogger<ViewKickOffMeetingTask> _logger;
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly IGetTaskStatusService _getTaskStatusService;
    private readonly IUpdateTaskStatusService _updateTaskStatusService;
    private readonly ErrorService _errorService;
    
    public ProjectTaskStatus ProjectTaskStatus { get; set; }

    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    [BindProperty] public bool MarkAsCompleted { get; set; }

    public GetProjectByTaskResponse Project { get; set; }

    public ViewKickOffMeetingTask(
        IGetProjectByTaskService getProjectService,
        ILogger<ViewKickOffMeetingTask> logger,
        IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService,
        ErrorService errorService)
    {
        _logger = logger;
        _getTaskStatusService = getTaskStatusService;
        _updateTaskStatusService = updateTaskStatusService;
        _errorService = errorService;
        _getProjectService = getProjectService;
    }
    
    public async Task<ActionResult> OnGet()
    {
        _logger.LogMethodEntered();
        
        Project = await _getProjectService.Execute(ProjectId, TaskName.KickOffMeeting);

        var taskStatusResponse = await _getTaskStatusService.Execute(ProjectId, TaskName.KickOffMeeting.ToString());

        ProjectTaskStatus = taskStatusResponse.ProjectTaskStatus;
        MarkAsCompleted = ProjectTaskStatus == ProjectTaskStatus.Completed;

        return Page();
    }
    
    public async Task<ActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        ProjectTaskStatus = MarkAsCompleted ? ProjectTaskStatus.Completed : ProjectTaskStatus.InProgress;

        await _updateTaskStatusService.Execute(ProjectId, new UpdateTaskStatusRequest
        {
            TaskName = TaskName.KickOffMeeting.ToString(),
            ProjectTaskStatus = ProjectTaskStatus
        });
        return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
    }

    
}   

