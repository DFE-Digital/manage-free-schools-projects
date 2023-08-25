using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectTask;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/projects/{projectId}/tasks")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        public readonly IUpdateProjectTaskService _updateProjectTaskService;
        public readonly ILogger<ProjectTaskController> _logger;

        public ProjectTaskController(
            IUpdateProjectTaskService updateProjectTaskService,
            ILogger<ProjectTaskController> logger)
        {
            _updateProjectTaskService = updateProjectTaskService;
            _logger = logger;
        }

        [HttpPatch]
        public async Task<ActionResult> PatchTask(string projectId, UpdateProjectTasksRequest request)
        {
            _logger.LogMethodEntered();

            await _updateProjectTaskService.Execute(projectId, request);

            return new OkResult();
        }
    }
}
