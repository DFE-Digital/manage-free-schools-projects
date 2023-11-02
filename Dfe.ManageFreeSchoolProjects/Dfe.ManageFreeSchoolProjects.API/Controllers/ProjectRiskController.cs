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
        private readonly IGetProjectRiskService _getProjectRiskService;

        public ProjectRiskController(
            ICreateProjectRiskService createProjectRiskService,
            IGetProjectRiskService getProjectRiskService)
        {
            _createProjectRiskService = createProjectRiskService;
            _getProjectRiskService = getProjectRiskService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiSingleResponseV2<GetProjectRiskResponse>>> GetProjectRisk(string projectId)
        {
            var response = await _getProjectRiskService.Execute(projectId);

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
