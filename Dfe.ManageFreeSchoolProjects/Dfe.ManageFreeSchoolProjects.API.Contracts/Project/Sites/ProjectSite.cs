using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites
{
    public class ProjectSite
    {
        public ProjectSiteAddress Address { get; set; } = new();
    }

    public class ProjectSiteAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string TownOrCity { get; set; }
        public string Postcode { get; set; }
    }
}
