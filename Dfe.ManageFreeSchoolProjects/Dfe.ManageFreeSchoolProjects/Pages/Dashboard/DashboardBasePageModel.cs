﻿using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardBasePageModel : PageModel
    {
        [BindProperty(Name = "search-by-project")]
        public string ProjectSearchTerm { get; set; }

        [BindProperty(Name = "search-by-region")]
        public List<string> RegionSearchTerm { get; set; } = new();

        public DashboardModel Dashboard { get; set; } = new();

        protected readonly ICreateUserService _createUserService;
        protected readonly IGetDashboardService _getDashboardService;

        public DashboardBasePageModel(
            ICreateUserService createUserService,
            IGetDashboardService getDashboardAllService)
        {
            _createUserService = createUserService;
            _getDashboardService = getDashboardAllService;
        }

        protected async Task LoadDashboard(GetDashboardServiceParameters parameters)
        {
            var username = User.Identity.Name.ToString();
            await _createUserService.Execute(username);

            parameters.Project = ProjectSearchTerm;
			parameters.Region = RegionSearchTerm.Count > 0 ? RegionSearchTerm.First() : null;

            var projects = await _getDashboardService.Execute(parameters);

            Dashboard = new DashboardModel()
            {
                Projects = projects,
                ProjectSearchTerm = ProjectSearchTerm,
                RegionSearchTerm = RegionSearchTerm,
            };
        }
    }
}
