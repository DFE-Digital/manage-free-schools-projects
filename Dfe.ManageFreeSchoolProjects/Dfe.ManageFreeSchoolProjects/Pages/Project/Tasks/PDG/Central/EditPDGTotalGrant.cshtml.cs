using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditPDGTotalGrant : PageModel
{
    public void OnGet()
    {
        
    }

    public object CurrentFreeSchoolName { get; set; }
    public object ProjectId { get; }
}