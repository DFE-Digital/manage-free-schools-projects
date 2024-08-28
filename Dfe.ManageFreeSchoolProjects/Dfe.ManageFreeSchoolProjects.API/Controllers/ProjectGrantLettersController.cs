using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.GrantLetters;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/client/projects/{projectId}/grant-letters")]
[ApiController]
public class ProjectGrantLettersController(ILogger<ProjectGrantLettersController> logger, IProjectGrantLettersService grantLettersService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiSingleResponseV2<GrantLetters>>> GetGrantLetters(string projectId)
    {
        logger.LogMethodEntered();

        var grantLetters = await grantLettersService.Get(projectId);

        var result = new ApiSingleResponseV2<ProjectPayments>(grantLetters);
        return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
    }
}