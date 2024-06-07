using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.ProjectStatus;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/client/updateprojectstatus")]
[ApiController]

public class ProjectStatusController : ControllerBase
{
    private readonly IUpdateProjectStatusService _updateProjectStatusService;
    private readonly ILogger<ProjectStatusController> _logger;
    
    public ProjectStatusController(ILogger<ProjectStatusController> logger, IUpdateProjectStatusService updateProjectStatusService)
    {
        _logger = logger;
        _updateProjectStatusService = updateProjectStatusService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ApiSingleResponseV2<UpdateProjectStatusResponse>>> UpdateProjectStatus(string projectId, UpdateProjectStatusRequest request)
    {
        _logger.LogMethodEntered();

        await _updateProjectStatusService.Execute(projectId, request);

        return new OkResult();
    }
    
}