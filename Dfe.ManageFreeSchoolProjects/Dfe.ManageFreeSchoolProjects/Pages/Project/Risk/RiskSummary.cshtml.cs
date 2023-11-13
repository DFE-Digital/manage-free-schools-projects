using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class RiskSummaryModel : PageModel
    {
        private readonly IGetProjectRiskService _getProjectRiskRatingService;

        private readonly ICreateProjectRiskCache _createProjectRiskCache;
        private readonly IGetProjectOverviewService _getProjectOverviewService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectRiskResponse ProjectRisk { get; set; }

        public string SchoolName { get; set; }

        public DateTime? RiskDate { get; set; }

        public int Entry { get; set; }

        public RiskSummaryModel(
            IGetProjectRiskService getProjectRiskRatingService,
            ICreateProjectRiskCache createProjectRiskCache,
            IGetProjectOverviewService getProjectOverviewService)
        {
            _getProjectRiskRatingService = getProjectRiskRatingService;
            _createProjectRiskCache = createProjectRiskCache;
            _getProjectOverviewService = getProjectOverviewService;
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
            var projectOverview = await _getProjectOverviewService.Execute(ProjectId);

            SchoolName = projectOverview.ProjectStatus.CurrentFreeSchoolName;

            if (projectRiskResponse != null)
            {
                ProjectRisk = projectRiskResponse;
                RiskDate = projectRiskResponse.Date;
            }

            return Page();
        }
    }
}
