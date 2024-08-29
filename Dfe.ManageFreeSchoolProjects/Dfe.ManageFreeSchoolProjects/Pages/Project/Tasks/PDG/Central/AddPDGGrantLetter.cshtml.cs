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

    public PdgGrantLetters PdgGrantLetters { get; set; }

    [BindProperty(Name = "date-signed-initial-grant-letter", BinderType = typeof(DateInputModelBinder))]
    public DateTime? InitialGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "date-signed-full-grant-letter", BinderType = typeof(DateInputModelBinder))]
    public DateTime? FullGrantLetterDateSigned { get; set; }

    [BindProperty(Name = "initial-grant-letter-saved-to-workspaces-folder")]
    public bool? InitialGrantLetterSavedToWorkspaces { get; set; }

    [BindProperty(Name = "full-grant-letter-saved-to-workspaces-folder")]
    public bool? FullGrantLetterSavedToWorkspaces { get; set; }

    public async Task<IActionResult> OnGet()
    {
        PdgGrantLetters = await grantLettersService.Get(ProjectId);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        await grantLettersService.Update(ProjectId, PdgGrantLetters);

        return Redirect(RouteConstants.EditPDGGrantLetters);
    }
}