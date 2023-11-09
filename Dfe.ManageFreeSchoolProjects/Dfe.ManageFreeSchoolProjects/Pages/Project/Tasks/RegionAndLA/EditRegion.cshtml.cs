using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.RegionLocalAuthority;

public class EditRegion : PageModel
{
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<ViewSchoolTask> _logger;
    private readonly IUpdateProjectByTaskService _updateProjectByTaskService;
    private readonly ErrorService _errorService;

    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    [BindProperty(Name = "region")]
    [Display(Name = "region")]
    [Required(ErrorMessage = "Select the region of the free school.")]
    public string Region { get; set; }

    public GetProjectByTaskResponse Project { get; set; }
    

    public EditRegion(IGetProjectByTaskService getProjectService,
        ILogger<ViewSchoolTask> logger,
        IUpdateProjectByTaskService updateProjectByTaskService,
        ErrorService errorService)
    {
        _getProjectService = getProjectService;
        _logger = logger;
        _updateProjectByTaskService = updateProjectByTaskService;
        _errorService = errorService;
    }

    public async Task<ActionResult> OnGet()
    {
        Project = await _getProjectService.Execute(ProjectId);

        if (!string.IsNullOrEmpty(Project.RegionAndLocalAuthority.Region))
        {
            Region = Project.RegionAndLocalAuthority.Region;
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        return Redirect(string.Format(RouteConstants.EditLocalAuthority, ProjectId) + $"?region={Region}");
    }
}