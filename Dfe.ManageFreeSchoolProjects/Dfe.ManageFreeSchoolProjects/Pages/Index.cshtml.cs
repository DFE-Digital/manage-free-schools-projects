using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.Reports;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.FeatureManagement;

namespace Dfe.BuildFreeSchools.Pages
{
	public class IndexModel(
		IGetDashboardService getDashboardService,
		ICreateUserService createUserService,
		IGetLocalAuthoritiesService getLocalAuthoritiesService,
		IGetProjectManagersService getProjectManagersService,
		IAllProjectsReportService allProjectsReportService,
		IFeatureManager featureManager,
		ILogger<IndexModel> logger,
		IDashboardFiltersCache dashboardFiltersCache)
		: DashboardBasePageModel(createUserService, getDashboardService, getLocalAuthoritiesService,
			getProjectManagersService, featureManager, dashboardFiltersCache)
	{
		private readonly IDashboardFiltersCache _dashboardFiltersCache = dashboardFiltersCache;

		public async Task<IActionResult> OnGetAsync()
		{
			logger.LogMethodEntered();

			try
			{
				await AddUser();
				await LoadPage();

				var filtersCache = _dashboardFiltersCache.Get();
				filtersCache.NavigatedAwayFromDashboard = false;
				_dashboardFiltersCache.Update(filtersCache);
			}
			catch (Exception ex)
			{
				logger.LogErrorMsg(ex);
				throw;
			}

			return Page();
		}

		public async Task<IActionResult> OnGetMovePage()
		{
			logger.LogMethodEntered();

			try
			{
				await LoadPage();
			}
			catch (Exception ex)
			{
				logger.LogErrorMsg(ex);
				throw;
			}

			return Page();
		}

		public async Task<IActionResult> OnPostSearch()
		{
			logger.LogMethodEntered();

			try
			{
				await LoadPage();
			}
			catch (Exception ex)
			{
				logger.LogErrorMsg(ex);
				throw;
			}

			return Page();
		}

		public async Task<IActionResult> OnGetClearFilters()
		{
			logger.LogMethodEntered();
			try
			{
				_dashboardFiltersCache.Delete();
				await LoadPage();
			}
			catch (Exception ex)
			{
				logger.LogErrorMsg(ex);
				throw;
			}

			return Page();
		}

		public async Task<IActionResult> OnGetDownloadFile()
		{
			logger.LogMethodEntered();
			try
			{
				var now = DateTime.Now.Date.ToString("yyyy-MM-dd");
				var fileName = $"{now}-mfsp-all-projects-export.xlsx";

				var stream = await allProjectsReportService.Execute();
				return File(stream, "application/octet-stream", fileName);
			}
			catch (Exception ex)
			{
				logger.LogErrorMsg(ex);
				throw;
			}
		}
		
		public async Task<IActionResult>OnGetDownloadFilteredFile(string projectIds)
		{
			logger.LogMethodEntered();
			try
			{
				var now = DateTime.Now.Date.ToString("yyyy-MM-dd");
				var fileName = $"{now}-mfsp-filtered-projects-export.xlsx";

				var stream = await allProjectsReportService.ExecuteWithFilter(projectIds);
				return File(stream, "application/octet-stream", fileName);
			}
			catch (Exception ex)
			{
				logger.LogErrorMsg(ex);
				throw;
			}
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