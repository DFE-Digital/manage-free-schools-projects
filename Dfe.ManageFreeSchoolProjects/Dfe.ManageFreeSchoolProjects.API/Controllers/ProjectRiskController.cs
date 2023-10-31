using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/projects/{projectId}/risk")]
    [ApiController]
    public class ProjectRiskController : ControllerBase
    {
        private readonly ICreateProjectRiskService _createProjectRiskService;

        public ProjectRiskController(ICreateProjectRiskService createProjectRiskService)
        {
            _createProjectRiskService = createProjectRiskService;
        }

        [HttpGet]
        public ActionResult<ApiSingleResponseV2<GetProjectRiskResponse>> GetProjectRisk(string projectId)
        {
            var response = new GetProjectRiskResponse()
            {
                Date = DateTime.Now,
                GovernanceAndSuitability = new ProjectRiskEntryResponse()
                {
                    Summary = "Governance and suitability risk summary",
                    RiskRating = ProjectRiskRating.Green
                },
                Education = new ProjectRiskEntryResponse()
                {
                    Summary = "Education risk summary",
                    RiskRating = ProjectRiskRating.Red
                },
                Finance = new ProjectRiskEntryResponse()
                {
                    Summary = "Finance risk summary",
                    RiskRating = ProjectRiskRating.AmberRed
                },
                Overall = new ProjectRiskEntryResponse()
                {
                    Summary = "Overall risk summary",
                    RiskRating = ProjectRiskRating.AmberGreen
                },
                RiskAppraisalFormSharepointLink = "https://www.google.com"
            };

            return new ObjectResult(new ApiSingleResponseV2<GetProjectRiskResponse>(response))
            { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        public async Task<ActionResult<ApiSingleResponseV2<CreateProjectRiskResponse>>> PostProjectRisk(string projectId, CreateProjectRiskRequest request)
        {
            var response = await _createProjectRiskService.Execute(projectId, request);

            return new ObjectResult(new ApiSingleResponseV2<CreateProjectRiskResponse>(response))
            { StatusCode = StatusCodes.Status201Created };
        }
    }
}
