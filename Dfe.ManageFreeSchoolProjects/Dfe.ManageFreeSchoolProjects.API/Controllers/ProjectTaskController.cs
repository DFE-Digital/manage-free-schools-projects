using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/projects/{projectId}/tasks")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        public readonly IUpdateProjectByTaskService _updateProjectTaskService;
        public readonly IGetProjectByTaskService _getProjectByTaskService;
        public readonly ILogger<ProjectTaskController> _logger;

        public ProjectTaskController(
            IUpdateProjectByTaskService updateProjectTaskService,
            IGetProjectByTaskService getProjectByTaskService,
            ILogger<ProjectTaskController> logger)
        {
            _updateProjectTaskService = updateProjectTaskService;
            _getProjectByTaskService = getProjectByTaskService;
            _logger = logger;
        }

        [HttpPatch]
        public async Task<ActionResult> PatchTask(string projectId, UpdateProjectByTaskRequest request)
        {
            _logger.LogMethodEntered();

            await _updateProjectTaskService.Execute(projectId, request);

            return new OkResult();
        }

        [HttpGet]
        public async Task<ActionResult<ApiSingleResponseV2<GetProjectByTaskResponse>>> GetProjectByTask(string projectId)
        {
            _logger.LogMethodEntered();

            var projectByTask = await _getProjectByTaskService.Execute(projectId);

            if (projectByTask == null) 
            {
                _logger.LogInformation("No project could be found for the given project id {projectId}", projectId);
                return new NotFoundResult();
            }

            var result = new ApiSingleResponseV2<GetProjectByTaskResponse>(projectByTask);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet]
        [Route("summary")]
        public ActionResult<ApiSingleResponseV2<ProjectByTaskSummaryResponse>> GetProjectTaskListSummary(string projectId)
        {
            _logger.LogMethodEntered();

            var taskSummary = new ProjectByTaskSummaryResponse()
            {
                School = new TaskSummaryResponse() { Name = "CHEESE", Status = ProjectTaskStatus.InProgress },
                Construction = new TaskSummaryResponse() { Name = "CHEESE", Status = ProjectTaskStatus.InProgress }
            };

            var result = new ApiSingleResponseV2<ProjectByTaskSummaryResponse>(taskSummary);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
