﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.RegionAndLA;

public class EditRegion : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    [BindProperty(Name = "region")]
    [Required(ErrorMessage = "Select the region of the free school.")]
    public string Region { get; set; }

    public string CurrentFreeSchoolName { get; set; }
    
<<<<<<< HEAD
    public GetProjectByTaskResponse Project { get; set; }
    
=======
>>>>>>> main
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<ViewSchoolTask> _logger;
    private readonly ErrorService _errorService;

    public EditRegion(IGetProjectByTaskService getProjectService, ILogger<ViewSchoolTask> logger, ErrorService errorService)
    {
        _getProjectService = getProjectService;
        _logger = logger;
        _errorService = errorService;
    }

    public async Task<ActionResult> OnGet()
    {
        _logger.LogMethodEntered();

<<<<<<< HEAD
        Project = await _getProjectService.Execute(ProjectId);
        CurrentFreeSchoolName = Project.School.CurrentFreeSchoolName;
        TempData["CurrentFreeSchoolName"] = CurrentFreeSchoolName;

        if (!string.IsNullOrEmpty(Project.RegionAndLocalAuthority.Region))
        {
            Region = Project.RegionAndLocalAuthority.Region;
        }

=======
        var project = await _getProjectService.Execute(ProjectId);
        CurrentFreeSchoolName = project.School.CurrentFreeSchoolName;
        Region = project.RegionAndLocalAuthority.Region;
        
        TempData["CurrentFreeSchoolName"] = CurrentFreeSchoolName;
        
>>>>>>> main
        return Page();
    }

    public IActionResult OnPost()
    {
        _logger.LogMethodEntered();
     
        if (!ModelState.IsValid)
        {
            CurrentFreeSchoolName = TempData.Peek("CurrentFreeSchoolName") as string;
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }
        
<<<<<<< HEAD
        return Redirect(string.Format(RouteConstants.EditLocalAuthority, ProjectId) + $"?region={Region}");
=======
        return Redirect(string.Format(RouteConstants.EditLocalAuthority, ProjectId, Region));
>>>>>>> main
    }
}