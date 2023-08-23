using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IGetDashboardByUserService _getDashboardByUser;
        private readonly IGetDashboardAllService _getDashboardAll;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            IGetDashboardByUserService getDashboardByUser, 
            IGetDashboardAllService getDashboardAll,
            ILogger<DashboardController> logger)
        {
            _getDashboardByUser = getDashboardByUser;
            _getDashboardAll = getDashboardAll;
            _logger = logger;
        }

        [HttpGet]
        [Route("byuser/{userId}")]
        public async Task<ActionResult<ApiResponseV2<GetDashboardResponse>>> GetProjectsByUser(string userId)
        {
            _logger.LogMethodEntered();

            var projects = await _getDashboardByUser.Execute(userId);

            return GetResponse(projects);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ApiResponseV2<GetDashboardResponse>>> GetAllProjects()
        {
            _logger.LogMethodEntered();

            var projects = await _getDashboardAll.Execute();

            return GetResponse(projects);
        }

        private ActionResult<ApiResponseV2<GetDashboardResponse>> GetResponse(List<GetDashboardResponse> projects)
        {
            var response = new ApiResponseV2<GetDashboardResponse>(projects, null);

            _logger.LogInformation($"Found {projects.Count} projects");

            return Ok(response);
        }
    }
}
