using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;


[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/client/contacts")]
[ApiController]

public class ContactsController : ControllerBase
{
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(ILogger<ContactsController> logger)
    {
        
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiSingleResponseV2<GetContactsByRefResponse>>> GetContacts(string projectId)
    {
        _logger.LogMethodEntered();
        
        var response = await _getContactsService.Execute(projectId);
        
        return new ObjectResult(new ApiSingleResponseV2<GetContactsByRefResponse>(response))
            { StatusCode = StatusCodes.Status200OK };
    }
    
    [HttpPatch]
    public async Task<ActionResult<ApiSingleResponseV2<GetContactsByRefResponse>>> UpdateContacts(string projectId)
    {
        _logger.LogMethodEntered();
    }
}