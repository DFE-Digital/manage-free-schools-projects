using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditPDGGrantLetter(IGrantLettersService grantLettersService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public string CurrentFreeSchoolName { get; set; }

    public ProjectGrantLetters GrantLetters { get; set; }

    [BindProperty(Name = "date-signed-initial-grant-letter", BinderType = typeof(DateInputModelBinder))]
    public DateTime? InitialGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "date-signed-full-grant-letter", BinderType = typeof(DateInputModelBinder))]
    public DateTime? FinalGrantLetterDateSigned { get; set; }

    [BindProperty]
    public bool InitialGrantLetterSavedToWorkplaces { get; set; }

    [BindProperty]
    public bool FinalGrantLetterSavedToWorkspaces { get; set; }

    public async Task<IActionResult> OnGet()
    {
        GrantLetters = await grantLettersService.Get(ProjectId);

        InitialGrantLetterDateSigned = GrantLetters.InitialGrantLetterDate;
        FinalGrantLetterDateSigned = GrantLetters.FinalGrantLetterDate;
        InitialGrantLetterSavedToWorkplaces = GrantLetters.InitialGrantLetterSavedToWorkplaces ?? false;
        FinalGrantLetterSavedToWorkspaces = GrantLetters.FinalGrantLetterSavedToWorkplaces ?? false;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var updatedGrantLetter = new ProjectGrantLetters
        {
            InitialGrantLetterDate = InitialGrantLetterDateSigned,
            FinalGrantLetterDate = FinalGrantLetterDateSigned,
            InitialGrantLetterSavedToWorkplaces = InitialGrantLetterSavedToWorkplaces,
            FinalGrantLetterSavedToWorkplaces = FinalGrantLetterSavedToWorkspaces
        };

        await grantLettersService.UpdateGrantLetters(ProjectId, updatedGrantLetter);

        return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
    }
}