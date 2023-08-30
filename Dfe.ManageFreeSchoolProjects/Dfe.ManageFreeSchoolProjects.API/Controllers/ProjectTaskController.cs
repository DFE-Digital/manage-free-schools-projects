using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
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

        [HttpGet]
        [Route("list/summary")]
        public ActionResult<ApiSingleResponseV2<ProjectTaskListSummaryResponse>> GetProjectTaskListSummary(string projectId)
        {
            _logger.LogMethodEntered();

            var taskSummary = new ProjectTaskListSummaryResponse()
            {
                Tasks = new TaskSummaryCollectionResponse()
                {
                    School = new TaskSummaryResponse() { Name = "School", Status = ProjectTaskStatus.NotStarted },
                    Construction = new TaskSummaryResponse() { Name = "Construction", Status = ProjectTaskStatus.InProgress }
                }
            };

            var result = new ApiSingleResponseV2<ProjectTaskListSummaryResponse>(taskSummary);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
