using Azure;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
	{
        private readonly ICreateProjectService _createProjectService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(
            ICreateProjectService createProject,
            ILogger<ProjectController> logger)
		{
            _createProjectService = createProject;
            _logger = logger;
		}

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateProject(CreateProjectRequest createProjectRequest)
        {
            _logger.LogMethodEntered();
            
            var createResult = await _createProjectService.Execute(createProjectRequest);

            foreach (ProjectResponseDetails proj in createResult.Projects)
            {
                if (proj.ProjectCreateState == ProjectCreateState.Exists)
                {
                    return new ObjectResult(createResult)
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity
                    };
                }
            }

            var response = new ApiSingleResponseV2<CreateProjectResponse>(createResult);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}

