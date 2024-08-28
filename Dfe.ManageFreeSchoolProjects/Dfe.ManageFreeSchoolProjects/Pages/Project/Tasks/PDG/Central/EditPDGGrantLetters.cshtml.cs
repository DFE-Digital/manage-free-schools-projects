using System;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditPDGGrantLetters(ILogger<EditPDGGrantLetters> logger) : PageModel
{
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    public string CurrentFreeSchoolName { get; set; }
    

    public IActionResult OnGet()
    {
        logger.LogMethodEntered();
        return Page();
    }

    public IActionResult OnPost()
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