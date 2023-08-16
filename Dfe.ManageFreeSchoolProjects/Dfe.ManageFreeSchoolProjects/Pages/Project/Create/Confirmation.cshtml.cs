using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class ConfirmationModel : PageModel
    {
        public CreateProjectCacheItem Project { get; set; }

        private ICreateProjectCache _createProjectCache;

        public ConfirmationModel(ICreateProjectCache createProjectCache)
        {
            _createProjectCache = createProjectCache;
        }

        public void OnGet()
        {
            Project = _createProjectCache.Get();
        }
    }
}
