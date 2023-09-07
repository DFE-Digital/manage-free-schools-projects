using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardModel
    {
        public string Header { get; set; }
        public List<GetDashboardResponse> Projects { get; set; }

        public string ProjectSearchTerm { get; set; }

        public List<string> RegionSearchTerm { get; set; } = new();
    }
}
