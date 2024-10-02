using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.CentralRoute;

public class ApplicationWaveViewModel(ICreateProjectCache createProjectCache) : CreateProjectBaseModel(createProjectCache)
{
    [BindProperty(Name = "application-wave")]
    public string ApplicationWave { get; set; }
    
    public IActionResult OnGet()
    {
        BackLink = GetPreviousPage(CreateProjectPageName.ApplicationWave);

        var project = CreateProjectCache.Get();

        ApplicationWave = project.ApplicationWave;

        return Page();
    }

    public IActionResult OnPost()
    {
        BackLink = GetPreviousPage(CreateProjectPageName.ApplicationWave);

        var projCache = CreateProjectCache.Get();

        projCache.ApplicationWave = ApplicationWave;
        
        CreateProjectCache.Update(projCache);

        return Redirect(GetNextPage(CreateProjectPageName.ApplicationWave));
    }
}