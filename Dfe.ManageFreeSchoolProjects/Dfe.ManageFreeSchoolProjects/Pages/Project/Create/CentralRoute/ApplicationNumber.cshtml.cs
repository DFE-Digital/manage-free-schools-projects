using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.CentralRoute;

public class ApplicationNumberViewModel(ICreateProjectCache createProjectCache) : CreateProjectBaseModel(createProjectCache)
{
    [BindProperty(Name = "application-number")]
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
        
        var project = CreateProjectCache.Get();

        project.ApplicationNumber = ApplicationNumber;
        
        CreateProjectCache.Update(project);

        return Redirect(GetNextPage(CreateProjectPageName.ApplicationNumber));
    }
}