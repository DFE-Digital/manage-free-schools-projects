using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IGetDashboardByUser _getDashboardByUser;

        public DashboardController(IGetDashboardByUser getDashboardByUser)
        {
            _getDashboardByUser = getDashboardByUser;
        }

        [HttpGet]
        [Route("byuser/{userId}")]
        public async Task<ActionResult<ApiResponseV2<GetDashboardByUserResponse>>> GetProjectsByUser(string userId)
        {
            var projects = await _getDashboardByUser.Execute(userId);

            var response = new ApiResponseV2<GetDashboardByUserResponse>(projects, null);

            return Ok(response);
        }
    }
}
