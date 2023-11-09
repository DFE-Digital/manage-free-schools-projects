using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.RegionLocalAuthority;

public class EditLocalAuthority : PageModel
{
    public string Region { get; set; }

    public IActionResult OnGet(string region)
    {
        return Page();
    }
}