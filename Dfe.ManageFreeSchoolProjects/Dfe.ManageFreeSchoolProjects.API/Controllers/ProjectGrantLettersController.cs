using System.Net;
using Dfe.ManageFreeSchoolProjects.API.Constants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.GrantLetters;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/client/projects/{projectId}/grant-letters")]
[ApiController]
public class ProjectGrantLettersController(ILogger<ProjectGrantLettersController> logger, IProjectGrantLettersService grantLettersService) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = PolicyNames.CanRead)]
    public async Task<ActionResult<ApiSingleResponseV2<ProjectGrantLetters>>> GetGrantLetters(string projectId)
    {
        logger.LogMethodEntered();

        var grantLetters = await grantLettersService.Get(projectId);

        var result = new ApiSingleResponseV2<ProjectGrantLetters>(grantLetters);
        return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
    }
    
    [HttpPut]
    [Authorize(Policy = PolicyNames.CanReadWrite)]
    public async Task<ActionResult> UpdateGrantLetters(string projectId, ProjectGrantLetters updatedGrantLetters)
    {
        logger.LogMethodEntered();
        
        await grantLettersService.UpdateGrantLetter(projectId, updatedGrantLetters);

        return NoContent();
    }
    
    [HttpPut]
    [Authorize(Policy = PolicyNames.CanReadWrite)]
    [Route("variation-letter")]
    public async Task<ActionResult> UpdateVariationLetter(string projectId, GrantVariationLetter newOrUpdatedVariationLetter)
    {
        logger.LogMethodEntered();
        
        await grantLettersService.UpdateVariationLetter(projectId, newOrUpdatedVariationLetter);

        return NoContent();
    }
}