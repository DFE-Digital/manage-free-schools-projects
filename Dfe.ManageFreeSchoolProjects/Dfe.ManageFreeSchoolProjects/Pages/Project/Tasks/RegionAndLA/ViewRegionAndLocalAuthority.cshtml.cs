using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.RegionLocalAuthority;

public class ViewRegionAndLocalAuthority : PageModel
{
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<ViewSchoolTask> _logger;
    private readonly IGetTaskStatusService _getTaskStatusService;
    private readonly IUpdateTaskStatusService _updateTaskStatusService;
    private readonly ErrorService _errorService;
    public GetProjectByTaskResponse Project { get; set; }
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    public bool MarkAsComplete { get; set; }

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
        
        Project = await _getProjectService.Execute(ProjectId);
        
        return Page();
    }
}