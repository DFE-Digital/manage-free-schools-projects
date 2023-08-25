using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/projects/{projectId}")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IGetProjectService _getProjectService;

        public ProjectController(
            IGetProjectService getProjectService,
            ILogger<ProjectController> logger)
        {
            _getProjectService = getProjectService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string projectId)
        {
            _logger.LogMethodEntered();

            var project = await _getProjectService.Execute(projectId);

            var result = new ApiSingleResponseV2<GetProjectResponse>(project);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
