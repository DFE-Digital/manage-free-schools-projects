using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddRiskCheckModel : PageModel
    {
        private readonly ICreateProjectRiskCache _createProjectRiskCache;
        private readonly ICreateProjectRiskService _createProjectRiskService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public CreateRiskCacheItem ProjectRisk { get; set; }

        public AddRiskCheckModel(ICreateProjectRiskCache createProjectRiskCache, ICreateProjectRiskService createProjectRiskService)
        {
            _createProjectRiskCache = createProjectRiskCache;
            _createProjectRiskService = createProjectRiskService;
        }

        public void OnGet()
        {
            ProjectRisk = _createProjectRiskCache.Get();
        }

        public async Task<IActionResult> OnPost()
        {
            var riskToCreate = _createProjectRiskCache.Get();

            var request = new CreateProjectRiskRequest()
            {
                GovernanceAndSuitability = new ProjectRiskEntryRequest()
                {
                    Summary = riskToCreate.GovernanceAndSuitability.Summary,
                    RiskRating = riskToCreate.GovernanceAndSuitability.RiskRating
                },
                Education = new ProjectRiskEntryRequest()
                {
                    Summary = riskToCreate.Education.Summary,
                    RiskRating = riskToCreate.Education.RiskRating
                },
                Finance = new ProjectRiskEntryRequest()
                {
                    Summary = riskToCreate.Finance.Summary,
                    RiskRating = riskToCreate.Finance.RiskRating
                },
                Overall = new ProjectRiskEntryRequest()
                {
                    Summary = riskToCreate.Overall.Summary,
                    RiskRating = riskToCreate.Overall.RiskRating
                },
                RiskAppraisalFormSharepointLink = riskToCreate.RiskAppraisalFormSharepointLink
            };

            await _createProjectRiskService.Execute(ProjectId, request);

            return Redirect($"/projects/{ProjectId}/risk/summary");
        }
    }
}
