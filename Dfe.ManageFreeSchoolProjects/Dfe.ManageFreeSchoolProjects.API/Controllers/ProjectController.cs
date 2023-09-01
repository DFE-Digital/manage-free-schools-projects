using Azure;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
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
        public ActionResult CreateProject(CreateProjectRequest createProjectRequest)
        {
            _logger.LogMethodEntered();

            var result = _createProjectService.Execute(createProjectRequest);

            foreach (ProjectResponseDetails proj in result.Result.Projects)
            {
                if (proj.ProjectCreateState == ProjectCreateState.Exists)
                {
                    return new ObjectResult(result.Result)
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity
                    };
                }
            }

            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}

