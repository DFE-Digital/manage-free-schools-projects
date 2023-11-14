using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class GetProjectByTaskResponse
    {
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public TrustTask Trust { get; set; }
        public RegionAndLocalAuthorityTask RegionAndLocalAuthority { get; set; }
        public RiskAppraisalMeetingTask RiskAppraisalMeetingTask { get; set; }
    }
}
