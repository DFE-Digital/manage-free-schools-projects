using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class RiskSummaryModel : PageModel
    {
        private readonly IGetProjectRiskRatingService _getProjectRiskRatingService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectRiskResponse ProjectRiskRating { get; set; }

        public RiskSummaryModel(IGetProjectRiskRatingService getProjectRiskRatingService)
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
