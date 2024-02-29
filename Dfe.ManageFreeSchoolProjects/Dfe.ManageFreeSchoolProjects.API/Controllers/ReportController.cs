using Dfe.ManageFreeSchoolProjects.API.UseCases.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IAllProjectsReportService _allProjectsReportService;

        public ReportController(IAllProjectsReportService allProjectsReportService)
        {
            _allProjectsReportService = allProjectsReportService;
        }

        [HttpGet("all-projects-export")]
        public async Task<FileStreamResult> GetAllProjects()
        {
            var excelStream = await _allProjectsReportService.Execute();
            excelStream.Position = 0;

            var now = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var fileName = $"{now}-mfsp-all-projects-export.xlsx";

            return File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
