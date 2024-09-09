using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class AddPDGGrantLetter(
    IGrantLettersService grantLettersService, 
    IGetProjectByTaskService getProjectService, 
    ILogger<AddPDGPaymentModel> logger, 
    ErrorService errorService) 
    : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public string CurrentFreeSchoolName { get; set; }

    public ProjectGrantLetters GrantLetters { get; set; }

    [BindProperty(Name = "date-signed-initial-grant-letter", BinderType = typeof(DateInputModelBinder))]
    [Display(Name = "Date")]
    public DateTime? InitialGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "date-signed-full-grant-letter", BinderType = typeof(DateInputModelBinder))]
    [Display(Name = "Date")]
    public DateTime? FullGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "initial-grant-letter-saved-to-workplaces-folder")]
    public bool InitialGrantLetterSavedToWorkplaces { get; set; }

    [BindProperty(Name = "full-grant-letter-saved-to-workplaces-folder")]
    public bool FullGrantLetterSavedToWorkplaces { get; set; }

    public async Task<ActionResult> OnGet()
    {
        logger.LogMethodEntered();
        await LoadSchoolName();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        logger.LogMethodEntered();

        if (!ModelState.IsValid)
        {
            errorService.AddErrors(ModelState.Keys, ModelState);
            await LoadSchoolName();
            return Page();
        }

        var newGrantLetter = new ProjectGrantLetters
        {
            InitialGrantLetterDate = InitialGrantLetterDateSigned,
            FinalGrantLetterDate = FullGrantLetterDateSigned,
            InitialGrantLetterSavedToWorkplaces = InitialGrantLetterSavedToWorkplaces,
            FinalGrantLetterSavedToWorkplaces = FullGrantLetterSavedToWorkplaces
        };

        await grantLettersService.UpdateGrantLetters(ProjectId, newGrantLetter);

        TempData["GrantLetterAdded"] = true;

        return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
    }
    private async Task LoadSchoolName()
    {
        var project = await getProjectService.Execute(ProjectId, TaskName.PDG);
        CurrentFreeSchoolName = project.SchoolName;
    }

}