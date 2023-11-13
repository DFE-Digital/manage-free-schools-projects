using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddGovernanceAndSuitabilityRiskModel : AddRiskBaseModel
    {
        private readonly ICreateProjectRiskCache _createProjectRiskCache;
        private readonly ErrorService _errorService;

        public AddGovernanceAndSuitabilityRiskModel(ICreateProjectRiskCache createProjectRiskCache, ErrorService errorService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _errorService = errorService;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            Summary = existingCacheItem.GovernanceAndSuitability.Summary;
            RiskRating = ((int)existingCacheItem.GovernanceAndSuitability.RiskRating).ToString();
            SchoolName = existingCacheItem.SchoolName;
        }

        public IActionResult OnPost()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            if (!ModelState.IsValid)
            {
                SchoolName = existingCacheItem.SchoolName;
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            existingCacheItem.GovernanceAndSuitability = new()
            {
                Summary = Summary,
                RiskRating = (ProjectRiskRating)int.Parse(RiskRating)
            };

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect(GetNextPage(RiskPageName.GovernanceAndSuitability, existingCacheItem));
        }
    }
}
