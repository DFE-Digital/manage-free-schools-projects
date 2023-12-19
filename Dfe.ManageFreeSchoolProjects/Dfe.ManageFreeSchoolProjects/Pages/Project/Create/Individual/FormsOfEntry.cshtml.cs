using System.ComponentModel;
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
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "forms-of-entry")]
    [DisplayName("Forms of entry")]
    [ValidText(100)]
    public string FormsOfEntry { get; set; }
    

    public FormsOfEntryModel(ICreateProjectCache createProjectCache, ErrorService errorService)
        :base(createProjectCache)
    {
        _errorService = errorService;
    }
    
    public IActionResult OnGet()
    {
        if (!IsUserAuthorised())
        {
            return new UnauthorizedResult();
        }

        
        var project = _createProjectCache.Get();

        FormsOfEntry = project.FormsOfEntry;

        BackLink = GetPreviousPage(CreateProjectPageName.FormsOfEntry);

        return Page();
    }

    public IActionResult OnPost()
    {
        var project = _createProjectCache.Get();

        BackLink = GetPreviousPage(CreateProjectPageName.FormsOfEntry);

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (string.IsNullOrEmpty(FormsOfEntry))
            FormsOfEntry = string.Empty;
        
        project.FormsOfEntry = FormsOfEntry;
        _createProjectCache.Update(project);
        
        return Redirect(GetNextPage(CreateProjectPageName.FormsOfEntry));
    }

}