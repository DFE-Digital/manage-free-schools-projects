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

    [BindProperty(Name = "initial-grant-letter-saved-to-workspaces-folder")]
    public bool? InitialGrantLetterSavedToWorkplaces { get; set; }

    [BindProperty(Name = "full-grant-letter-saved-to-workspaces-folder")]
    public bool? FullGrantLetterSavedToWorkspaces { get; set; }

    public async Task<IActionResult> OnGet()
    {
        GrantLetters = await grantLettersService.Get(ProjectId);
        
        //todo: set fields from get

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var updatedGrantLetter = new VariationGrantLetter
        {
            Variation = VariationGrantLetter.GrantLetterVariation.Initial,
            InitialGrantLetterDate = InitialGrantLetterDateSigned,
            FinalGrantLetterDate = FinalGrantLetterDateSigned,
            SavedToWorkplacesFolder = FullGrantLetterSavedToWorkspaces
        };

        await grantLettersService.Update(ProjectId, updatedGrantLetter);

        return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
    }
}