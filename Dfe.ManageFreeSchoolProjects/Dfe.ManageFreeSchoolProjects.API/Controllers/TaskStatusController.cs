using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;

[Route("api/v{version:apiVersion}/task/status")]
[ApiController]
public class TaskStatusController : ControllerBase
{
    private readonly UpdateTaskStatusService _updateTaskStatusService;
    private readonly ILogger<TaskStatusController> _logger;

    public TaskStatusController(UpdateTaskStatusService updateTaskStatusService, ILogger<TaskStatusController> logger)
    {
        _updateTaskStatusService = updateTaskStatusService;
        _logger = logger;
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateTaskStatus(string projectId, [FromBody] string taskName,
        [FromBody] string status)
    {
        _logger.LogMethodEntered();

        await _updateTaskStatusService.Execute(projectId, taskName, ProjectTaskStatus.Completed);

        return new OkResult();
    }
}