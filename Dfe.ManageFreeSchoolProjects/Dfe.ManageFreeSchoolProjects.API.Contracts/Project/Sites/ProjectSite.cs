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
        public DateTime? DatePlanningPermissionObtained { get; set; }
        public DateTime? StartDateOfOccupation { get; set; }
    }

    public class ProjectSiteAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        public override string ToString()
        {
            var addressValues = new List<string>
            {
                AddressLine1,
                AddressLine2,
                Postcode
            };

            var allValuesUnset = addressValues.All(string.IsNullOrWhiteSpace);

            if (allValuesUnset)
            {
                return string.Empty;
            }

            return string.Join(", ", addressValues.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}
