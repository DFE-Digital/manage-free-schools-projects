using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.BuildFreeSchools.Pages
{
    public class IndexModel : DashboardBasePageModel
	{
		private readonly IGetLocalAuthoritiesService _getLocalAuthoritiesService;
		private readonly ILogger<IndexModel> _logger;

        public IndexModel(
			IGetDashboardService getDashboardService, 
			ICreateUserService createUserService,
			IGetLocalAuthoritiesService getLocalAuthoritiesService,
            ILogger<IndexModel> logger) : base(createUserService, getDashboardService)
        {
			_getLocalAuthoritiesService = getLocalAuthoritiesService;
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

		public async Task<JsonResult> OnGetLocalAuthoritiesByRegion(string regions)
		{
			if (string.IsNullOrEmpty(regions))
			{
				return new JsonResult(new List<string>());
			}

			var regionsToSearch = regions.Split(",").ToList();

			var regionResponse = await _getLocalAuthoritiesService.Execute(regionsToSearch);

			var regionList = regionResponse.LocalAuthorities.Select(l => l.Name).ToList();

			var result = new JsonResult(regionList);

			return result;
		}

        protected async Task LoadPage()
		{
			await LoadDashboard(new GetDashboardServiceParameters());

			Dashboard.Header = "All projects";
		}
	}
}