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
        public readonly IUpdateProjectTaskService _updateProjectTaskService;
        public readonly IGetProjectByTaskService _getProjectByTaskService;
        public readonly ILogger<ProjectTaskController> _logger;

        public ProjectTaskController(
            IUpdateProjectTaskService updateProjectTaskService,
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

            var result = new ApiSingleResponseV2<GetProjectByTaskResponse>(projectByTask);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet]
        [Route("list/summary")]
        public ActionResult<ApiSingleResponseV2<ProjectTaskListSummaryResponse>> GetProjectTaskListSummary(string projectId)
        {
            _logger.LogMethodEntered();

            var taskSummary = new ProjectTaskListSummaryResponse()
            {
                School = new TaskSummaryResponse() { Name = "School", Status = ProjectTaskStatus.NotStarted },
                Construction = new TaskSummaryResponse() { Name = "Construction", Status = ProjectTaskStatus.InProgress }
            };

            var result = new ApiSingleResponseV2<ProjectTaskListSummaryResponse>(taskSummary);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
