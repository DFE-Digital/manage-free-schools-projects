using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardGetByUserModel : PageModel
    {
        private readonly IGetDashboardService _getDashboardService;
        private readonly ILogger<DashboardGetByUserModel> _logger;

        public DashboardModel Dashboard { get; set; }

        public DashboardGetByUserModel(
            IGetDashboardService getDashboardByUserService,
            ILogger<DashboardGetByUserModel> logger)
        {
            _getDashboardService = getDashboardByUserService;
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

            var parameters = new GetDashboardServiceParameters()
            {
                UserId = username
            };

            var projects = await _getDashboardService.Execute(parameters);

            Dashboard = new DashboardModel()
            {
                Header = "Your projects",
                Projects = projects
            };
        }
    }
}
