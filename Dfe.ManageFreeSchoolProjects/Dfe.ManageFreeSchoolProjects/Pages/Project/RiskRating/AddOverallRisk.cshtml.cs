using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
    public class AddOverallRiskModel : PageModel
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

        public AddOverallRiskModel(ICreateProjectRiskCache createProjectRiskCache, ErrorService errorService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _errorService = errorService;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            if (existingCacheItem.Overall != null)
            {
                Summary = existingCacheItem.Overall.Summary;
                RiskRating = ((int)existingCacheItem.Overall.RiskRating).ToString();
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

            existingCacheItem.Overall = new()
            {
                Summary = Summary,
                RiskRating = (ProjectRiskRating)int.Parse(RiskRating)
            };

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect($"/projects/{ProjectId}/risk-rating/check/add");
        }
    }
}
