using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class FinalFinancePlanTask
    {
        public bool? ConfirmedTrustHasProvidedFinalPlan { get; set; }
        public DateTime? Grade6SignedOffFinalPlanDate { get; set; }
        public bool? SentFinalPlanToRevenueFundingMailbox { get; set; }
        public bool? AddedAnyUnderwrittenPlacesToFinalPlan { get; set; }
        public bool? SavedFinalPlanInWorkplacesFolder { get; set; }
        public DateTime? ExpectedDateGrade6WillSignOffTheFinalPlan { get; set; }
    }
}
