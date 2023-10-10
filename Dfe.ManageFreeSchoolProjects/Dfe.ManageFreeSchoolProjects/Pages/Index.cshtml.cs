using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.BuildFreeSchools.Pages
{
    public class IndexModel : DashboardBasePageModel
	{
		private readonly ILogger<IndexModel> _logger;

        public IndexModel(
			IGetDashboardService getDashboardService, 
			ICreateUserService createUserService,
			IGetLocalAuthoritiesService getLocalAuthoritiesService,
            ILogger<IndexModel> logger) : base(createUserService, getDashboardService, getLocalAuthoritiesService)
        {
			_logger = logger;
        }

		public async Task<IActionResult> OnGetAsync()
		{
			_logger.LogMethodEntered();

			try
			{
				await AddUser();
				await LoadPage();
			}
			catch (Exception ex) 
			{
                _logger.LogErrorMsg(ex);
				throw;
            }

			return Page();
		}

		public async Task<IActionResult> OnGetMovePage()
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

		public async Task<IActionResult> OnPostSearch()
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

        public async Task<IActionResult> OnGetClearFilters()
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

        protected async Task LoadPage()
		{
            var parameters = new LoadDashboardParameters()
            {
                GetDashboardServiceParameters = new GetDashboardServiceParameters(),
                Url = string.Empty
            };

			await LoadDashboard(parameters);

			Dashboard.Header = "All projects";
        }
	}
}