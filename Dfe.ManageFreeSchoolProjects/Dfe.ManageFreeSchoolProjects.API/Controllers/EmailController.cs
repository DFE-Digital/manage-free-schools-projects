using Dfe.ManageFreeSchoolProjects.API.UseCases.Email;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/email")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailController> _logger;

    public EmailController(IEmailService emailService, ILogger<EmailController> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

     [HttpPost]
     public async Task<ActionResult> SendEmail(string email)
     {
         _logger.LogMethodEntered();

         if (email.IsNullOrEmpty()) return BadRequest("Email is required.");
         
         try
         {
             await _emailService.SendEmail(email);
             return Ok();
         }
         catch (Exception ex)
         {
             _logger.LogErrorMsg(ex);
             return new ObjectResult("Failed to send email. Check Gov Notify service.")
                 { StatusCode = StatusCodes.Status500InternalServerError };
         }
     }
}