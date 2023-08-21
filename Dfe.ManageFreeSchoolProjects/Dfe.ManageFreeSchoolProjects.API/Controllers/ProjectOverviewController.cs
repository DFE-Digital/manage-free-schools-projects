using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/project/overview")]
    [ApiController]
    public class ProjectOverviewController : ControllerBase
    {
        [HttpGet]
        [Route("/{projectId}")]
        public async Task GetProjectOverview(string projectId)
        {
            
        }
    }
}
