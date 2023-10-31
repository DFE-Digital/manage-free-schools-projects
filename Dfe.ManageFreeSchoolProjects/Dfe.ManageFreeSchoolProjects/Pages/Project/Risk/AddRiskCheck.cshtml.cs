using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddRiskCheckModel : PageModel
    {
        private readonly ICreateProjectRiskCache _createProjectRiskCache;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public CreateRiskCacheItem ProjectRisk { get; set; }

        public AddRiskCheckModel(ICreateProjectRiskCache createProjectRiskCache)
        {
            _createProjectRiskCache = createProjectRiskCache;
        }

        public void OnGet()
        {
            ProjectRisk = _createProjectRiskCache.Get();
        }
    }
}
