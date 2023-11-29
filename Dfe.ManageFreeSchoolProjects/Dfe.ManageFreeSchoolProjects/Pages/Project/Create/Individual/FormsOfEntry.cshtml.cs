using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class FormsOfEntryModel : CreateProjectBaseModel
    
{
    private readonly ICreateProjectCache _createProjectCache;
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "forms-of-entry")]
    [ValidText(100, "Forms of entry")]
    [Required]
    public string FormsOfEntry { get; set; }
    

    public FormsOfEntryModel(ICreateProjectCache createProjectCache, ErrorService errorService)
    {
        _createProjectCache = createProjectCache;
        _errorService = errorService;
    }
    
    public IActionResult OnGet()
    {
        if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
        {
            return new UnauthorizedResult();
        }
        
        var project = _createProjectCache.Get();

        FormsOfEntry = project.FormsOfEntry;

        BackLink = GetPreviousPage(CreateProjectPageName.FormsOfEntry, project.Navigation);

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        var project = _createProjectCache.Get();
        project.FormsOfEntry = FormsOfEntry;
        _createProjectCache.Update(project);
        
        return Redirect(GetNextPage(CreateProjectPageName.FormsOfEntry));
    }

}