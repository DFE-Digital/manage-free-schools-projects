using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System;
using System.Threading.Tasks;
using Dfe.BuildFreeSchools.Pages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardGetByUserModel(
        ICreateUserService createUserService,
        IGetDashboardService getDashboardService,
        IGetLocalAuthoritiesService getLocalAuthoritiesService,
        IGetProjectManagersService getProjectManagersService,
        ILogger<DashboardGetByUserModel> logger,
        IFeatureManager featureManager,
        IDashboardFiltersCache dashboardFiltersCache)
        : DashboardBasePageModel(createUserService, getDashboardService, getLocalAuthoritiesService,
            getProjectManagersService, featureManager, dashboardFiltersCache)
    {
        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogMethodEntered();

            try
            {
                await AddUser();
                await LoadPage();
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
                await LoadPage();
            }
            catch (Exception ex)
            {
                logger.LogErrorMsg(ex);
                throw;
            }

            return Page();
        }

        private async Task LoadPage()
        {
            var username = User.Identity.Name;

            var parameters = new LoadDashboardParameters
            {
                GetDashboardServiceParameters = new GetDashboardServiceParameters
                {
                    UserId = username
                },
                Url = "/my"
            };

            await LoadDashboard(parameters);

            Dashboard.Header = "Your projects";
        }
    }
}
