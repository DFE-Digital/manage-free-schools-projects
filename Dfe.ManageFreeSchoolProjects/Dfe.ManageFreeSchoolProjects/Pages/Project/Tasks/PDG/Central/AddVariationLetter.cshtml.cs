using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class AddVariationLetter(IGrantLettersService grantLettersService, ILogger<AddVariationLetter> logger) : PageModel
{
    public string CurrentFreeSchoolName { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string ProjectId { get; set; }

    [BindProperty(Name = "due-date-variation-letter",  BinderType = typeof(DateInputModelBinder))]
    public DateTime? DueDateOfVariationLetter { get; set; }

    [BindProperty]
    public bool VariationLetterSavedToWorkplaces { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        logger.LogMethodEntered();

        try
        {
            var variationNumber = TempData.Peek("VariationLetterToAdd") as string;

            var newVariationLetter = new GrantVariationLetter
            {
                Variation = ParseVariation(variationNumber),
                LetterDate = DueDateOfVariationLetter,
                SavedToWorkplacesFolder = VariationLetterSavedToWorkplaces
            };

            await grantLettersService.UpdateVariationLetter(ProjectId, newVariationLetter);

            return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
        }
        catch (Exception e)
        {
            logger.LogErrorMsg(e);
            throw;
        }
    }

    private static GrantVariationLetter.LetterVariation ParseVariation(string variationNumber)
    {
        if (!int.TryParse(variationNumber, out var variationAsInt)) 
            return GrantVariationLetter.LetterVariation.NotSet;
        
        if (Enum.IsDefined(typeof(GrantVariationLetter.LetterVariation), variationAsInt))
            return (GrantVariationLetter.LetterVariation)variationAsInt;

        return GrantVariationLetter.LetterVariation.NotSet;
    }

}