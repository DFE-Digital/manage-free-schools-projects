using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
    public class AddRiskAppraisalFormModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return Redirect($"/projects/{ProjectId}/risk-rating/overall/add");
        }
    }
}
