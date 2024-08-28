using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class AddPDGGrantLetter : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    public string CurrentFreeSchoolName { get; set; }
    
    public GrantLetters GrantLetters { get; set; }
    
    public bool? InitialGrantLetterSavedToWorkspacesFolder { get; set; }

    public bool? FullGrantLetterSavedToWorkspacesFolder { get; set; }

    public void OnGet()
    {
        GrantLetters = new GrantLetters(); //todo: get data
    }

}