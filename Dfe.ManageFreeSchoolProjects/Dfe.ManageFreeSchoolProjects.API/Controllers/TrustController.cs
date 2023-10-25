using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/trust/{trn}")]
    [ApiController]
    public class TrustTaskController : ControllerBase
    {
        private readonly IGetTrustByRefService _getTrustByRefService;
        private readonly ILogger<TrustTaskController> _logger;

        public TrustTaskController(
            IGetTrustByRefService getTrustByRefService,
            ILogger<TrustTaskController> logger)
        {
            _getTrustByRefService = getTrustByRefService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiSingleResponseV2<GetTrustByRefResponse>>> Execute(string trn)
        {
            _logger.LogMethodEntered();

            var trustByRef = await _getTrustByRefService.Execute(trn);

            if (trustByRef == null) 
            {
                _logger.LogInformation("No trust could be found for the given trust ref {trn}", trn);
                return new NotFoundResult();
            }

            var result = new ApiSingleResponseV2<GetTrustByRefResponse>(trustByRef);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
