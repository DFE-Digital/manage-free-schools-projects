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
        private readonly IGetDashboardService _getDashboard;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            IGetDashboardService getDashboardAll,
            ILogger<DashboardController> logger)
        {
            _getDashboard = getDashboardAll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseV2<GetDashboardResponse>>> GetAllProjects(
            string userId,
            string region,
            string project)
        {
            _logger.LogMethodEntered();

            var parameters = new GetDashboardParameters()
            {
                UserId = userId,
                Region = region,
                Project = project
            };

            var projects = await _getDashboard.Execute(parameters);

            return GetResponse(projects);
        }

        private ActionResult<ApiResponseV2<GetDashboardResponse>> GetResponse(List<GetDashboardResponse> projects)
        {
            var response = new ApiResponseV2<GetDashboardResponse>(projects, null);

            _logger.LogInformation("Found {ProjectCount} projects", projects.Count);

            return Ok(response);
        }
    }
}
