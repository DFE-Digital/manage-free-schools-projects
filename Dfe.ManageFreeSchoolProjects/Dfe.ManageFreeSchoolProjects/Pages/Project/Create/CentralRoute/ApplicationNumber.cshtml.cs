using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.CentralRoute;

public class ApplicationNumberViewModel(ICreateProjectCache createProjectCache, ErrorService errorService) : CreateProjectBaseModel(createProjectCache)
{
    [BindProperty(Name = "application-number")]
    [Display(Name = "application number")]
    [StringLength(10, ErrorMessage = "The application number must be 10 characters or less.")]
    public string ApplicationNumber { get; set; }
    
    public IActionResult OnGet()
    {
        BackLink = GetPreviousPage(CreateProjectPageName.ApplicationNumber);
        
        var project = CreateProjectCache.Get();
        
        ApplicationNumber = project.ApplicationNumber;
        
        return Page();
    }

    public IActionResult OnPost()
    {
        BackLink = GetPreviousPage(CreateProjectPageName.ApplicationNumber);

        if (ModelState.IsValid == false)
        {
            errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }   
        
        var project = CreateProjectCache.Get();

        project.ApplicationNumber = ApplicationNumber;
        
        CreateProjectCache.Update(project);

        return Redirect(GetNextPage(CreateProjectPageName.ApplicationNumber));
    }
}