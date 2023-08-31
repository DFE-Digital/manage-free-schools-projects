using Azure;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
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
        private readonly IGetProjectService _getProjectService;
        private readonly ICreateProjectService _createProjectService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(
            ICreateProjectService createProject,
            IGetProjectService getProjectService,
            ILogger<ProjectController> logger)
		{
            _createProjectService = createProject;
            _getProjectService = getProjectService;
            _logger = logger;
		}

        [HttpPost]
        [Route("create/individual")]
        public ActionResult CreateProject(CreateProjectRequest createProjectRequest)
        {
            _logger.LogMethodEntered();

            _createProjectService.Execute(createProjectRequest);

            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}

