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

public class EditPDGGrantLetter(
    IGrantLettersService grantLettersService,
    IGetProjectByTaskService getProjectService,
    ErrorService errorService,
    ILogger<EditPDGGrantLetter> logger
    ) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public string CurrentFreeSchoolName { get; set; }

    public ProjectGrantLetters GrantLetters { get; set; }

    [BindProperty(Name = "date-signed-initial-grant-letter", BinderType = typeof(DateInputModelBinder))]
    [Display(Name = "Date when DfE signed the initial grant letter")]
    [Required]
    public DateTime? InitialGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "date-signed-full-grant-letter", BinderType = typeof(DateInputModelBinder))]
    [Display(Name = "Date when DfE signed the final grant letter")]
    public DateTime? FinalGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "initial-grant-letter-saved-to-workplaces-folder")]
    public bool InitialGrantLetterSavedToWorkplaces { get; set; }

    [BindProperty(Name = "full-grant-letter-saved-to-workplaces-folder")]
    public bool FinalGrantLetterSavedToWorkplaces { get; set; }

    public async Task<IActionResult> OnGet()
    {
        logger.LogMethodEntered();

        if (!User.IsInRole(RolesConstants.GrantManagers))
        {
            return new UnauthorizedResult();
        }

        GrantLetters = await grantLettersService.Get(ProjectId);

        InitialGrantLetterDateSigned = GrantLetters.InitialGrantLetterDate;
        FinalGrantLetterDateSigned = GrantLetters.FinalGrantLetterDate;
        InitialGrantLetterSavedToWorkplaces = GrantLetters.InitialGrantLetterSavedToWorkplaces ?? false;
        FinalGrantLetterSavedToWorkplaces = GrantLetters.FinalGrantLetterSavedToWorkplaces ?? false;

        await LoadSchoolName();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        logger.LogMethodEntered();

        if (!User.IsInRole(RolesConstants.GrantManagers))
        {
            return new UnauthorizedResult();
        }

        if (!ModelState.IsValid)
        {
            errorService.AddErrors(ModelState.Keys, ModelState);
            await LoadSchoolName();
            return Page();
        }

        var updatedGrantLetter = new ProjectGrantLetters
        {
            InitialGrantLetterDate = InitialGrantLetterDateSigned,
            FinalGrantLetterDate = FinalGrantLetterDateSigned,
            InitialGrantLetterSavedToWorkplaces = InitialGrantLetterSavedToWorkplaces,
            FinalGrantLetterSavedToWorkplaces = FinalGrantLetterSavedToWorkplaces
        };

        await grantLettersService.UpdateGrantLetters(ProjectId, updatedGrantLetter);

        return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
    }
    private async Task LoadSchoolName()
    {
        var project = await getProjectService.Execute(ProjectId, TaskName.PDG);
        CurrentFreeSchoolName = project.SchoolName;
    }
}