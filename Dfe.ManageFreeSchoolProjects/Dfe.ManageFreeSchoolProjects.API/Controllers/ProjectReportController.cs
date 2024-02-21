using Dfe.ManageFreeSchoolProjects.API.UseCases.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/reports")]
    [ApiController]
    public class ProjectReportController : ControllerBase
    {
        private readonly IAllProjectsReportService _allProjectsReportService;

        public ProjectReportController(IAllProjectsReportService allProjectsReportService)
        {
            _allProjectsReportService = allProjectsReportService;
        }

        [HttpGet("all")]
        public async Task<HttpResponseMessage> GetAllProjects()
        {
            await _allProjectsReportService.Execute();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
