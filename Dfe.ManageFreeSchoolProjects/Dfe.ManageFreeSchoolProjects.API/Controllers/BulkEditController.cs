using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/bulkedit")]
    [ApiController]
    public class BulkEditController(ILogger<BulkEditController> logger, IBulkEditValidation bulkEditValidation) : ControllerBase
    {

        [HttpPost]
        [Route("validate")]
        public async Task<ActionResult<ApiSingleResponseV2<BulkEditValidateResponse>>> validate(BulkEditValidateRequest request)
        {
            logger.LogMethodEntered();

            if (request == null)
            {
                return BadRequest("Request body is required.");
            }

            var response = await bulkEditValidation.Execute(request);

            return new ObjectResult(new ApiSingleResponseV2<BulkEditValidateResponse>(response))
            { StatusCode = StatusCodes.Status200OK };
        }
    }
}
