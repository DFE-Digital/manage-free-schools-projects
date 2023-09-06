using Dfe.ManageFreeSchoolProjects.Pages.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using DocumentFormat.OpenXml.Drawing;

namespace Dfe.BuildFreeSchools.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGetDashboardService _getDashboardAllService;
		private readonly ICreateUserService _createUserService;
        private readonly ILogger<IndexModel> _logger;

        public DashboardModel Dashboard { get; set; }

        public IndexModel(
			IGetDashboardService getDashboardAllService, 
			ICreateUserService createUserService,
            ILogger<IndexModel> logger)
        {
			_getDashboardAllService = getDashboardAllService;
			_createUserService = createUserService;
			_logger = logger;
        }

		public async Task<IActionResult> OnGetAsync()
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
			await _createUserService.Execute(username);

			var projects = await _getDashboardAllService.Execute(new GetDashboardServiceParameters());

            Dashboard = new DashboardModel()
            {
                Header = "All projects",
				Projects = projects
            };
		}
	}
}