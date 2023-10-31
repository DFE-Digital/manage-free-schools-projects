using Azure;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/projects/{projectId}/risk")]
    public class ProjectRiskController : ControllerBase
    {
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
    }
}
