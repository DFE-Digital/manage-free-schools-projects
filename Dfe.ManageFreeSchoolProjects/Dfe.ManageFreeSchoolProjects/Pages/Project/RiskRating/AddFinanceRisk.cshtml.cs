using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
    public class AddFinanceRiskModel : PageModel
    {
        private readonly ErrorService _errorService;
        private readonly ICreateProjectRiskCache _createProjectRiskCache;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "risk-rating")]
        [Display(Name = "Risk rating")]
        [Required]
        public string RiskRating { get; set; }

        [BindProperty(Name = "summary")]
        public string Summary { get; set; }

        public AddFinanceRiskModel(ICreateProjectRiskCache createProjectRiskCache, ErrorService errorService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _errorService = errorService;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            if (existingCacheItem.Finance != null)
            {
                Summary = existingCacheItem.Finance.Summary;
                RiskRating = ((int)existingCacheItem.Finance.RiskRating).ToString();
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

            existingCacheItem.Finance = new()
            {
                Summary = Summary,
                RiskRating = (ProjectRiskRating)int.Parse(RiskRating)
            };

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect($"/projects/{ProjectId}/risk-rating/risk-appraisal-form/add");
        }
    }
}
