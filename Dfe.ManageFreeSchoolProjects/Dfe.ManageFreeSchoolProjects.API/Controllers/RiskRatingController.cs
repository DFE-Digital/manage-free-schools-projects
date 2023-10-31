using Azure;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/projects/{projectId}/risk-rating")]
    public class RiskRatingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ApiSingleResponseV2<GetProjectRiskRatingResponse>> GetProjectRiskRating(string projectId)
        {
            var response = new GetProjectRiskRatingResponse()
            {
                Date = DateTime.Now,
                GovernanceAndSuitability = new ProjectRiskRatingEntryResponse()
                {
                    Summary = "Governance and suitability risk summary",
                    RiskRating = ProjectRiskRating.Green
                },
                Education = new ProjectRiskRatingEntryResponse()
                {
                    Summary = "Education risk summary",
                    RiskRating = ProjectRiskRating.Red
                },
                Finance = new ProjectRiskRatingEntryResponse()
                {
                    Summary = "Finance risk summary",
                    RiskRating = ProjectRiskRating.AmberRed
                },
                Overall = new ProjectRiskRatingEntryResponse()
                {
                    Summary = "Overall risk summary",
                    RiskRating = ProjectRiskRating.AmberGreen
                },
                RiskAppraisalFormSharepointLink = "https://www.google.com"
            };

            return new ObjectResult(new ApiSingleResponseV2<GetProjectRiskRatingResponse>(response))
            { StatusCode = StatusCodes.Status200OK };
        }
    }
}
