using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Helpers
{
    public static class AssertionHelper
    {
        public static void AssertProjectSite(ProjectSite actual, ProjectSite expected)
        {
            actual.Address.Should().BeEquivalentTo(expected.Address);
            actual.StartDateOfSiteOccupation.Value.Date.Should().Be(expected.StartDateOfSiteOccupation.Value.Date);
        }
    }
}
