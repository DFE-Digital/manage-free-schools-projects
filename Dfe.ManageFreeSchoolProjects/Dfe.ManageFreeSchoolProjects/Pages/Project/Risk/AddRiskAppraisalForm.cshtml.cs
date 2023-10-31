using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
    public class AddRiskAppraisalFormModel : PageModel
    {
        private readonly ErrorService _errorService;
        private readonly ICreateProjectRiskCache _createProjectRiskCache;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "sharepoint-link")]
        public string SharepointLink { get; set; }

        public AddRiskAppraisalFormModel(ICreateProjectRiskCache createProjectRiskCache, ErrorService errorService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _errorService = errorService;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            SharepointLink = existingCacheItem.RiskAppraisalFormSharepointLink;
        }

        public IActionResult OnPost()
        {
            var existingCacheItem = _createProjectRiskCache.Get();
            existingCacheItem.RiskAppraisalFormSharepointLink = SharepointLink;

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect($"/projects/{ProjectId}/risk-rating/overall/add");
        }
    }
}
