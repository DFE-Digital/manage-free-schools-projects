using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Logging;
using DocumentFormat.OpenXml.Drawing;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardGetByUserModel : PageModel
    {
        private readonly IGetDashboardByUserService _getDashboardByUserService;
        private readonly ILogger<DashboardGetByUserModel> _logger;

        public DashboardModel Dashboard { get; set; }

        public DashboardGetByUserModel(
            IGetDashboardByUserService getDashboardByUserService,
            ILogger<DashboardGetByUserModel> logger)
        {
            _getDashboardByUserService = getDashboardByUserService;
            _logger = logger;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            _logger.LogMethodEntered();

            try
            {
                await LoadPage();
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }

            return Page();
        }

        private async Task LoadPage()
        {
            var username = User.Identity.Name.ToString();

            var projects = await _getDashboardByUserService.Execute(username);

            Dashboard = new DashboardModel()
            {
                Header = "Your projects",
                Projects = projects
            };
        }
    }
}
