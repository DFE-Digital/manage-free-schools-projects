using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class AddPDGGrantLetter(IGrantLettersService grantLettersService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public string CurrentFreeSchoolName { get; set; }

    public ProjectGrantLetters GrantLetters { get; set; }

    [BindProperty(Name = "date-signed-initial-grant-letter", BinderType = typeof(DateInputModelBinder))]
    public DateTime? InitialGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "date-signed-full-grant-letter", BinderType = typeof(DateInputModelBinder))]
    public DateTime? FullGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "initial-grant-letter-saved-to-workspaces-folder")]
    public bool InitialGrantLetterSavedToWorkspaces { get; set; }

    [BindProperty(Name = "full-grant-letter-saved-to-workspaces-folder")]
    public bool FullGrantLetterSavedToWorkspaces { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var newGrantLetter = new ProjectGrantLetters
        {
            InitialGrantLetterDate = InitialGrantLetterDateSigned,
            FinalGrantLetterDate = FullGrantLetterDateSigned,
            InitialGrantLetterSavedToWorkplaces = InitialGrantLetterSavedToWorkspaces,
            FinalGrantLetterSavedToWorkplaces = FullGrantLetterSavedToWorkspaces
        };

        await grantLettersService.UpdateGrantLetters(ProjectId, newGrantLetter);

        TempData["GrantLetterAdded"] = true;

        return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
    }
}