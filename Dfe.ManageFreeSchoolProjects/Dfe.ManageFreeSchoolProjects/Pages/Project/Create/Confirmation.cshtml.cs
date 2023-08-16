using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class ConfirmationModel : PageModel
    {
        public CreateProjectCacheItem Project { get; set; }

        private readonly ICreateProjectCache _createProjectCache;

        public ConfirmationModel(ICreateProjectCache createProjectCache)
        {
            _createProjectCache = createProjectCache;
        }

        public IActionResult OnGet()
        {
            Project = _createProjectCache.Get();

            return Page();
        }
    }
}
