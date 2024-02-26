using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class DraftGovernancePlanTask
    {
        public DateTime? ForecastDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string CommentsOnDecisionToApprove { get; set; }
        public string SharepointLink { get; set; }
    }
}
