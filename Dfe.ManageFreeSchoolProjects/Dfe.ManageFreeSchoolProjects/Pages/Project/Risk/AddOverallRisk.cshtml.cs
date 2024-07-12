using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddOverallRiskModel : PageModel
    {
		[BindProperty(SupportsGet = true, Name = "projectId")]
		public string ProjectId { get; set; }

		[BindProperty(Name = "risk-rating")]
		[Display(Name = "risk rating")]
		public string RiskRating { get; set; }

		[BindProperty(Name = "summary")]
		[Display(Name = "Summary")]
		[StringLength(5000, ErrorMessage = ValidationConstants.TextValidationMessage)]
		public string Summary { get; set; }

		public string SchoolName { get; set; }

		public string GetNextPage()
		{
			return string.Format(RouteConstants.ProjectRiskReview, ProjectId);
		}

		private readonly ErrorService _errorService;
        private readonly ICreateProjectRiskCache _createProjectRiskCache;

        public AddOverallRiskModel(ICreateProjectRiskCache createProjectRiskCache, ErrorService errorService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _errorService = errorService;
        }

        public void OnGet()
        {
            var existingCacheItem = _createProjectRiskCache.Get();

            Summary = existingCacheItem.Overall.Summary;
            RiskRating = existingCacheItem.Overall.RiskRating.ToIntString();
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

            existingCacheItem.Overall = new()
            {
                Summary = Summary,
                RiskRating = RiskRating.ToEnum<ProjectRiskRating>()
            };

            _createProjectRiskCache.Update(existingCacheItem);

            return Redirect(GetNextPage());
        }
    }
}
