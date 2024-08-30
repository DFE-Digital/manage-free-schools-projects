using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
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
    
    public async Task<IActionResult> OnGet()
    {
        GrantLetters = await grantLettersService.Get(ProjectId);
        
        return Page();
    }
}