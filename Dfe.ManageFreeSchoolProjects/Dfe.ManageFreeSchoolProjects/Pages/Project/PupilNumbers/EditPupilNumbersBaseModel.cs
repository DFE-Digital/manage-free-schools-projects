using Dfe.ManageFreeSchoolProjects.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    public class EditPupilNumbersBaseModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty]
        public string SchoolName { get; set; }

        protected IActionResult GoToViewPage()
        {
            return Redirect(string.Format(RouteConstants.ViewPupilNumbers, ProjectId) + "?fromSaveForm=true");
        }
    }
}
