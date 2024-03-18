using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites
{
    public class UpdateProjectSitesRequest
    {
        public ProjectSite PermenantSite { get; set; }
        public ProjectSite TemporarySite { get; set; }
    }
}
