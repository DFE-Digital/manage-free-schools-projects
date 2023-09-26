﻿using Dfe.ManageFreeSchoolProjects.Pages.Pagination;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardBasePageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(Name = "search-by-project", SupportsGet = true)]
        public string ProjectSearchTerm { get; set; }

        [BindProperty(Name = "search-by-region", SupportsGet = true)]
        public List<string> RegionSearchTerm { get; set; } = new();

        [BindProperty(Name = "search-by-local-authority", SupportsGet = true)]
        public List<string> LocalAuthoritySearchTerm { get; set; } = new();

        public DashboardModel Dashboard { get; set; } = new();

        protected readonly ICreateUserService _createUserService;
        protected readonly IGetDashboardService _getDashboardService;
        private readonly IGetLocalAuthoritiesService _getLocalAuthoritiesService;

        public DashboardBasePageModel(
            ICreateUserService createUserService,
            IGetDashboardService getDashboardAllService,
            IGetLocalAuthoritiesService getLocalAuthoritiesService)
        {
            _createUserService = createUserService;
            _getDashboardService = getDashboardAllService;
            _getLocalAuthoritiesService = getLocalAuthoritiesService;
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

        protected async Task AddUser()
        {
            var username = User.Identity.Name.ToString();
            await _createUserService.Execute(username);
        }

        protected async Task LoadDashboard(LoadDashboardParameters loadDashboardParameters)
        {
            var getDashboardServiceParameters = loadDashboardParameters.GetDashboardServiceParameters;

            getDashboardServiceParameters.Project = ProjectSearchTerm;
			getDashboardServiceParameters.Regions = RegionSearchTerm;
            getDashboardServiceParameters.LocalAuthorities = LocalAuthoritySearchTerm;
            getDashboardServiceParameters.Page = PageNumber;

            var response = await _getDashboardService.Execute(getDashboardServiceParameters);

            var paginationModel = PaginationMapping.ToModel(response.Paging);
            var query = BuildPaginationQuery();
            paginationModel.Url = $"{loadDashboardParameters.Url}{query}";

            Dashboard = new DashboardModel()
            {
                Projects = response.Data.ToList(),
                ProjectSearchTerm = ProjectSearchTerm,
                RegionSearchTerm = RegionSearchTerm,
                LocalAuthoritySearchTerm = LocalAuthoritySearchTerm,
                Pagination = paginationModel,
            };
        }

        private string BuildPaginationQuery()
        {
            QueryString query = new QueryString("?handler=movePage");

            if (!string.IsNullOrEmpty(ProjectSearchTerm))
            {
                query = query.Add("search-by-project", ProjectSearchTerm);
            }

            if (RegionSearchTerm.Any())
            {
                RegionSearchTerm.ForEach((r => query = query.Add("search-by-region", r)));
            }

            if (LocalAuthoritySearchTerm.Any())
            {
                LocalAuthoritySearchTerm.ForEach((l => query = query.Add("search-by-local-authority", l)));
            }

            return query.ToString();
        }

        protected class LoadDashboardParameters
        {
            public GetDashboardServiceParameters GetDashboardServiceParameters { get; set; }
            public string Url { get; set; }
        }
    }
}
