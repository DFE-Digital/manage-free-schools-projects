using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public class GetProjectResponse
    {
        public string SchoolType { get; set; }
        public string SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public string Nursery { get; set; }
        public string SixthForm { get; set; }
        public string CompanyName { get; set; }
        public string NumberOfCompanyMembers { get; set; }
        public string ProposedChairOfTrustees { get; set; }
        public string NameOfSite { get; set; }
        public string AddressOfSite { get; set; }
        public string PostcodeOfSite { get; set; }
        public string BuildingType { get; set; }
        public string TrustRef { get; set; }
        public string TrustLeadSponsor { get; set; }
        public string TrustName { get; set; }
        public string SiteMinArea { get; set; }
        public string TypeofWorksLocation { get; set; }
    }
}
