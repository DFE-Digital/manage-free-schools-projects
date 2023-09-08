using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.User;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardGetByUserModel : DashboardBasePageModel
    {
        private readonly ILogger<DashboardGetByUserModel> _logger;

        public DashboardGetByUserModel(
            ICreateUserService createUserService,
            IGetDashboardService getDashboardService,
            ILogger<DashboardGetByUserModel> logger) : base(createUserService, getDashboardService)
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

        private async Task LoadPage()
        {
            var username = User.Identity.Name.ToString();

            var parameters = new GetDashboardServiceParameters()
            {
                UserId = username
            };

            await LoadDashboard(parameters);

            Dashboard.Header = "Your projects";
        }
    }
}
