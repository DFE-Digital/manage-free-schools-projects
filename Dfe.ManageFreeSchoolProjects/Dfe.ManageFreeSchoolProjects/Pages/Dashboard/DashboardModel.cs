using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using System.Collections.Generic;

namespace Dfe.ManageFreeSchoolProjects.Pages.Dashboard
{
    public class DashboardModel
    {
        public string Header { get; set; }
        public List<GetDashboardResponse> Projects { get; set; }
    }
}
