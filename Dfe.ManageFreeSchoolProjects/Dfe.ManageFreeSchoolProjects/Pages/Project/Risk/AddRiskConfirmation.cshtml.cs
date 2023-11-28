using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddRiskConfirmationModel : PageModel
    {
        [BindProperty(Name = "projectId", SupportsGet = true)]
        public string ProjectId { get; set; }

        public void OnGet()
        {
        }
    }
}
