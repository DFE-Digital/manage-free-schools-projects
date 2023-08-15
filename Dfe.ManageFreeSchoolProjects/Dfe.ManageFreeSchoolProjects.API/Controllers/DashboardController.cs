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
        private IGetDashboardAll _getDashboardAll;

        public DashboardController(IGetDashboardByUser getDashboardByUser, IGetDashboardAll getDashboardAll)
        {
            _getDashboardByUser = getDashboardByUser;
            _getDashboardAll = getDashboardAll;
        }

        [HttpGet]
        [Route("byuser/{userId}")]
        public async Task<ActionResult<ApiResponseV2<GetDashboardResponse>>> GetProjectsByUser(string userId)
        {
            var projects = await _getDashboardByUser.Execute(userId);

            return GetResponse(projects);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ApiResponseV2<GetDashboardResponse>>> GetAllProjects()
        {
            var projects = await _getDashboardAll.Execute();

            return GetResponse(projects);
        }

        public ActionResult<ApiResponseV2<GetDashboardResponse>> GetResponse(List<GetDashboardResponse> projects)
        {
            var response = new ApiResponseV2<GetDashboardResponse>(projects, null);

            return Ok(response);
        }
    }
}
