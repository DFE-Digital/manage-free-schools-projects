namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class DraftGovernancePlanTask
    {
        public bool? PlanReceivedFromTrust { get; set; }

        public DateTime? DateReceived { get; set; }

        public bool? PlanAssessedUsingTemplate { get; set; }

        public bool? PlanAndTemplateSharedWithExpert { get; set; }

        public bool? PlanAndTemplateSharedWithEsfa { get; set; }

        public bool? PlanFedBackToTrust { get; set; }

        public bool? SavedDocumentsInWorkplacesFolder { get; set; }

        public string CommentsOnDecisionToApprove { get; set; }


        public DateTime? ForecastDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string SharepointLink { get; set; }
    }
}
