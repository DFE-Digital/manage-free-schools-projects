using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
    public class RiskRatingSummaryModel : PageModel
    {
        private readonly IGetProjectRiskRatingService _getProjectRiskRatingService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectRiskRatingResponse ProjectRiskRating { get; set; }

        public RiskRatingSummaryModel(IGetProjectRiskRatingService getProjectRiskRatingService)
        {
            _getProjectRiskRatingService = getProjectRiskRatingService;
        }

        public async Task<IActionResult> OnGet()
        {
            ProjectRiskRating = await _getProjectRiskRatingService.Execute(ProjectId);

            return Page();
        }
    }
}
