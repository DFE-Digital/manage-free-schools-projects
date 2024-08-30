using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditPDGGrantLetters(ILogger<EditPDGGrantLetters> logger, IGrantLettersService grantLettersService) : PageModel
{
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    public string CurrentFreeSchoolName { get; set; }
    
    public ProjectGrantLetters GrantLetters { get; set; }

    public async Task<IActionResult> OnGet()
    {
        logger.LogMethodEntered();

        GrantLetters = await grantLettersService.Get(ProjectId);
        
        return Page();
    }

    public IActionResult OnPost(string action)
    {
        try
        {
            return Redirect(string.Format(RouteConstants.AddPDGGrantLetter, ProjectId));
        }
        catch (Exception ex)
        {
            logger.LogErrorMsg(ex);
            throw;
        }
    }

}