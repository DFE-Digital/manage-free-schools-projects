using Dfe.ManageFreeSchoolProjects.Pages.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
    public class AddRiskAppraisalFormModel : AddRiskBaseModel
    {
        private readonly ICreateProjectRiskCache _createProjectRiskCache;

        [BindProperty(Name = "sharepoint-link")]
        public string SharepointLink { get; set; }

        public AddRiskAppraisalFormModel(ICreateProjectRiskCache createProjectRiskCache)
        {
            _createProjectRiskCache = createProjectRiskCache;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            SharepointLink = existingCacheItem.RiskAppraisalFormSharepointLink;
            SchoolName = existingCacheItem.SchoolName;
        }

        public IActionResult OnPost()
        {
            var existingCacheItem = _createProjectRiskCache.Get();
            existingCacheItem.RiskAppraisalFormSharepointLink = SharepointLink;

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect(GetNextPage(RiskPageName.SharepointLink, existingCacheItem));
        }
    }
}
