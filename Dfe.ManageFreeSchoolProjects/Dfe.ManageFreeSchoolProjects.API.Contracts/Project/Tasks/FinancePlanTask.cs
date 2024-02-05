using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class FinancePlanTask
    {
        public YesNo? FinancePlanAgreed { get; set; }
        public DateTime? DateAgreed { get; set; }
        public YesNo? PlanSavedInWorkspaceFolder { get; set; }
        public YesNoNotApplicable? LocalAuthorityAgreedPupilNumbers { get; set; }
        public string CommentsOnDecisionToApprove { get; set; }
        public YesNo? TrustWillOptIntoRpa { get; set; }
    }
}
