using System.Text.Json;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;

[Route("api/v{version:apiVersion}/{projectId}/task/status")]
[ApiController]
public class TaskStatusController : ControllerBase
{
    private readonly IUpdateTaskStatusService _updateTaskStatusService;
    private readonly IGetTaskStatusService _getTaskStatusService;
    private readonly ILogger<TaskStatusController> _logger;

    public TaskStatusController(IUpdateTaskStatusService updateTaskStatusService, ILogger<TaskStatusController> logger,
        IGetTaskStatusService getTaskStatusService)
    {
        _updateTaskStatusService = updateTaskStatusService;
        _getTaskStatusService = getTaskStatusService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApiSingleResponseV2<TaskStatusResponse>>> GetTaskStatus([FromRoute] string projectId,
        string taskName)
    {
        _logger.LogMethodEntered();

        try
        {
            var response = new TaskStatusResponse
            {
                ProjectTaskStatus = await _getTaskStatusService.Execute(projectId, taskName)
            };

            return new ObjectResult(new ApiSingleResponseV2<TaskStatusResponse>(response))
                { StatusCode = StatusCodes.Status200OK };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateTaskStatus(string projectId, [FromBody] UpdateTaskStatusRequest request)
    {
        _logger.LogMethodEntered();

        try
        {
            await _updateTaskStatusService.Execute(projectId, request.TaskName, request.ProjectTaskStatus);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new OkResult();
    }
}