using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddGovernanceAndSuitabilityRiskModel : PageModel
    {
        private readonly ICreateProjectRiskCache _createProjectRiskCache;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "risk-rating")]
        [Display(Name = "Risk rating")]
        [Required]
        public string RiskRating { get; set; }

        [BindProperty(Name = "summary")]
        public string Summary { get; set; }

        public AddGovernanceAndSuitabilityRiskModel(ICreateProjectRiskCache createProjectRiskCache, ErrorService errorService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _errorService = errorService;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            if (existingCacheItem.GovernanceAndSuitability != null)
            {
                Summary = existingCacheItem.GovernanceAndSuitability.Summary;
                RiskRating = ((int)existingCacheItem.GovernanceAndSuitability.RiskRating).ToString();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var existingCacheItem = _createProjectRiskCache.Get();

            existingCacheItem.GovernanceAndSuitability = new()
            {
                Summary = Summary,
                RiskRating = (ProjectRiskRating)int.Parse(RiskRating)
            };

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect($"/projects/{ProjectId}/risk-rating/education/add");
        }
    }
}
