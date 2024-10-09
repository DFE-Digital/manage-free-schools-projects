using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

public class EditPDGVariationLetter(
    IGrantLettersService grantLettersService, 
    IGetProjectByTaskService getProjectService,
    ILogger<EditPDGVariationLetter> logger,
    ErrorService errorService) : PageModel
{
    [BindProperty(SupportsGet = true)] 
    public string ProjectId { get; set; }

    [BindProperty(SupportsGet = true)] 
    public string Variation { get; set; }

    public string CurrentFreeSchoolName { get; set; }

    [BindProperty(Name = "due-date-variation-letter", BinderType = typeof(DateInputModelBinder))]
    [Display(Name = "Due date of variation letter")]
    [Required]
    public DateTime? DueDateOfVariationLetter { get; set; }

    [BindProperty] 
    public bool VariationLetterSavedToWorkplaces { get; set; }

    public GrantVariationLetter VariationLetter { get; set; }

    public async Task<IActionResult> OnGet()
    {
        logger.LogMethodEntered();

        if (!User.IsInRole(RolesConstants.GrantManagers))
        {
            return new UnauthorizedResult();
        }

        var grantLetters = await grantLettersService.Get(ProjectId);

        var parsedVariation = ParseVariation(Variation);

        VariationLetter = grantLetters.VariationLetters.SingleOrDefault(x => x.Variation == parsedVariation);
        DueDateOfVariationLetter = VariationLetter?.LetterDate;
        VariationLetterSavedToWorkplaces = VariationLetter?.SavedToWorkplacesFolder ?? false;

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
        
        try
        {
            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                await LoadSchoolName();
                return Page();
            }

            var updatedVariationLetter = new GrantVariationLetter
            {
                Variation = ParseVariation(Variation),
                LetterDate = DueDateOfVariationLetter,
                SavedToWorkplacesFolder = VariationLetterSavedToWorkplaces
            };

            await grantLettersService.UpdateVariationLetter(ProjectId, updatedVariationLetter);

            return Redirect(string.Format(RouteConstants.EditPDGGrantLetters, ProjectId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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