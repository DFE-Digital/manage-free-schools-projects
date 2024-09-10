using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Payments;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/projects/{projectId}/payments")]
    [ApiController]
    public class ProjectPaymentsController : ControllerBase
    {
        private readonly IGetProjectPaymentsService _getProjectPaymentsService;
        private readonly IUpdateProjectPaymentsService _updateProjectPaymentsService;
        private readonly IAddProjectPaymentsService _addProjectPaymentsService;
        private readonly IDeleteProjectPaymentsService _deleteProjectPaymentsService;
        private readonly ILogger<ProjectPaymentsController> _logger;

        public ProjectPaymentsController(
            IGetProjectPaymentsService getProjectPaymentsService,
            IUpdateProjectPaymentsService updateProjectPaymentsService,
            IAddProjectPaymentsService addProjectPaymentsService,
            IDeleteProjectPaymentsService deleteProjectPaymentsService,
            ILogger<ProjectPaymentsController> logger)
        {
            _getProjectPaymentsService = getProjectPaymentsService;
            _updateProjectPaymentsService = updateProjectPaymentsService;
            _addProjectPaymentsService = addProjectPaymentsService;
            _deleteProjectPaymentsService = deleteProjectPaymentsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiSingleResponseV2<ProjectPayments>>> GetProjectPayments(string projectId)
        {
            _logger.LogMethodEntered();

            var payments = await _getProjectPaymentsService.Execute(projectId);

            var result = new ApiSingleResponseV2<ProjectPayments>(payments);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProjectPayments(string projectId, Payment payment)
        {
            _logger.LogMethodEntered();

            await _updateProjectPaymentsService.Execute(projectId, payment);

            return new OkResult();
        }

        [HttpPost]
        public async Task<ActionResult> AddProjectPayments(string projectId, Payment payment)
        {
            _logger.LogMethodEntered();

            await _addProjectPaymentsService.Execute(projectId, payment);

            return new OkResult();
        }

        [HttpDelete("{paymentIndex}")]
        public async Task<ActionResult> DeleteProjectPayments(string projectId, int paymentIndex)
        {
            _logger.LogMethodEntered();

            await _deleteProjectPaymentsService.Execute(projectId, paymentIndex);

            return new OkResult();
        }
    }
}
