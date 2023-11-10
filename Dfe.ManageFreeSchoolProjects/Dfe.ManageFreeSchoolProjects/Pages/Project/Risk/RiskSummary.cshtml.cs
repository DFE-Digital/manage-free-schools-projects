using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class RiskSummaryModel : PageModel
    {
        private readonly IGetProjectRiskService _getProjectRiskRatingService;

        private readonly ICreateProjectRiskCache _createProjectRiskCache;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectRiskResponse ProjectRisk { get; set; }

        public int Entry { get; set; }

        public RiskSummaryModel(
            IGetProjectRiskService getProjectRiskRatingService,
            ICreateProjectRiskCache createProjectRiskCache)
        {
            _getProjectRiskRatingService = getProjectRiskRatingService;
            _createProjectRiskCache = createProjectRiskCache;
        }

        public IActionResult OnGetNewConfigureRiskRating()
        {
            _createProjectRiskCache.Delete();

            return Redirect($"/projects/{ProjectId}/risk/governance-and-suitability/add");
        }

        public async Task<IActionResult> OnGet(int entry = 1)
        {
            Entry = entry;
            ProjectRisk = new GetProjectRiskResponse();

            var projectRiskResponse = await _getProjectRiskRatingService.Execute(ProjectId, entry);

            if (projectRiskResponse != null)
            {
                ProjectRisk = projectRiskResponse;
            }

            return Page();
        }
    }
}
