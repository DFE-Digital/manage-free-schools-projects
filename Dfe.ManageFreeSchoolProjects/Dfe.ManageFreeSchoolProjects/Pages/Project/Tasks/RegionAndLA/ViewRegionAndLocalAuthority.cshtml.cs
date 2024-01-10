using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.RegionAndLA;

public class ViewRegionAndLocalAuthority : PageModel
{
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<ViewSchoolTask> _logger;
    private readonly IGetTaskStatusService _getTaskStatusService;
    private readonly IUpdateTaskStatusService _updateTaskStatusService;
    private readonly ErrorService _errorService;
    
    private const string RegionAndLocalAuthorityTaskName = "RegionAndLocalAuthority";
    
    public GetProjectByTaskResponse Project { get; set; }
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    [BindProperty]
    public bool MarkAsComplete { get; set; }
    
    public ProjectTaskStatus ProjectTaskStatus { get; set; }

    public ViewRegionAndLocalAuthority(IGetProjectByTaskService getProjectService,
        ILogger<ViewSchoolTask> logger,
        IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService,
        ErrorService errorService)
    {
        _getProjectService = getProjectService;
        _logger = logger;
        _getTaskStatusService = getTaskStatusService;
        _updateTaskStatusService = updateTaskStatusService;
        _errorService = errorService;
    }
    
    public async Task<ActionResult> OnGet()
    {
        _logger.LogMethodEntered();
        
        Project = await _getProjectService.Execute(ProjectId, TaskName.RegionAndLocalAuthority);
        
        var taskStatusResponse = await _getTaskStatusService.Execute(ProjectId, RegionAndLocalAuthorityTaskName);

        ProjectTaskStatus = taskStatusResponse.ProjectTaskStatus;
        MarkAsComplete = ProjectTaskStatus == ProjectTaskStatus.Completed;
        
        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        ProjectTaskStatus = MarkAsComplete ? ProjectTaskStatus.Completed : ProjectTaskStatus.InProgress;

        await _updateTaskStatusService.Execute(ProjectId, new UpdateTaskStatusRequest
        {
            TaskName = RegionAndLocalAuthorityTaskName, ProjectTaskStatus = ProjectTaskStatus
        });
        return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
    }
}