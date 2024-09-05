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

public class AddVariationLetter(
    IGrantLettersService grantLettersService,
    ILogger<AddVariationLetter> logger,
    IGetProjectByTaskService getProjectService,
    ErrorService errorService) : PageModel
{
    public string CurrentFreeSchoolName { get; set; }

    [BindProperty(SupportsGet = true)] public string ProjectId { get; set; }

    [BindProperty(Name = "due-date-variation-letter", BinderType = typeof(DateInputModelBinder))]
    [Display(Name = "Date")]
    public DateTime? DueDateOfVariationLetter { get; set; }

    [BindProperty] public bool VariationLetterSavedToWorkplaces { get; set; }

    public async Task<ActionResult> OnGet()
    {
        logger.LogMethodEntered();
        await LoadSchoolName();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        logger.LogMethodEntered();

        try
        {
            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var variationNumber = TempData.Peek("VariationLetterToAdd") as string;

            var newVariationLetter = new GrantVariationLetter
            {
                Variation = ParseVariation(variationNumber),
                LetterDate = DueDateOfVariationLetter,
                SavedToWorkplacesFolder = VariationLetterSavedToWorkplaces
            };

            await grantLettersService.UpdateVariationLetter(ProjectId, newVariationLetter);

            TempData["VariationLetterAdded"] = true;
            TempData["VariationLetterNumberAdded"] = variationNumber;

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
        return Enum.TryParse<GrantVariationLetter.LetterVariation>(variationNumber, out var variationEnum)
            ? variationEnum
            : GrantVariationLetter.LetterVariation.NotSet;
    }

    private async Task LoadSchoolName()
    {
        var project = await getProjectService.Execute(ProjectId, TaskName.PDG);
        CurrentFreeSchoolName = project.SchoolName;
    }
}