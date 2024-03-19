using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Data.Entities.Existing
{
    public partial class Milestones
    {
        public bool FsgPreOpeningMilestonesFundingArrangementAgreedBetweenLaAndSponsor { get; set; }

        public bool? FsgPreOpeningMilestonesAdmissionsArrangementsRecommendedTemplate { get; set; }

        public bool? FsgPreOpeningMilestonesAdmissionsArrangementsComplyWithPolicies { get; set; }

        public bool? FsgPreOpeningMilestonesAdmissionsArrangementsSavedToWorkplaces { get; set; }

        public bool? FsgPreOpeningMilestonesMfadTailoredAModelFundingAgreement { get; set; }

        public bool? FsgPreOpeningMilestonesMfadSharedFaWithTheTrust { get; set; }

        public bool? FsgPreOpeningMilestonesMfadDraftedFaHealthCheck { get; set; }

        public bool? FsgPreOpeningMilestonesMfadSavedFaDocumentsInWorkspacesFolder { get; set; }

        public YesNo? FsgPreOpeningMilestonesMfadTrustAgreesWithModelFa { get; set; }

        public bool? FsgPreOpeningMilestonesScrReceived { get; set; }

        public bool? FsgPreOpeningMilestonesScrFulfilsSection10StatutoryDuty { get; set; }

        public bool? FsgPreOpeningMilestonesScrSavedFindingsInWorkplacesFolder { get; set; }

        public bool? FSGPreOpeningMilestonesGIASCheckedTrustInformation { get; set; }

        public bool? FSGPreOpeningMilestonesGIASApplicationFormSent { get; set; }

        public bool? FSGPreOpeningMilestonesGIASSavedToWorkspaces { get; set; }

        public bool? FSGPreOpeningMilestonesGIASURNSent { get; set; }

        public bool? FSGPreOpeningMilestonesEducationPlanInBrief { get; set; }

        public bool? FSGPreOpeningMilestonesEducationPolicesInBrief { get; set; }

        public bool? FSGPreOpeningMilestonesEducationBriefPupilAssessmentAndTrackingHistory { get; set; }

        public bool? FSGPreOpeningMilestonesEducationBriefSavedToWorkplaces { get; set; }

        public bool MAACheckedSubmittedArticlesMatch { get; set; }

        public bool MAAChairHaveSubmittedConfirmation { get; set; }

        public bool MAAArrangementsMatchGovernancePlans { get; set; }

        public YesNo? FinancePlanSavedInWorkplacesFolder { get; set; }

        public YesNoNotApplicable? LAAgreedPupilNumbers { get; set; }

        public YesNo? TrustOptInRPA { get; set; }

        public DateTime? RPAStartDate { get; set; }

        public string RPACoverType { get; set; }

        public bool? DraftGovernancePlanReceivedFromTrust { get; set; }

        public DateTime? DraftGovernancePlanReceivedDate { get; set; }

        public bool? DraftGovernancePlanAssessedUsingTemplate { get; set; }

        public bool? DraftGovernancePlanAndAssessmentSharedWithExpert { get; set; }

        public bool? DraftGovernancePlanAndAssessmentSharedWithEsfa { get; set; }

        public bool? DraftGovernancePlanFedBackToTrust { get; set; }

        public bool? DraftGovernancePlanDocumentsSavedInWorkplacesFolder { get; set; }

        public bool? FsgPreOpeningMilestonesImpactAssessmentDone { get; set; }

        public bool? FsgPreOpeningMilestonesImpactAssessmentSavedToWorkplaces { get; set; }

        public bool? EqualitiesAssessmentCompletedEPR { get; set; }

        public bool? EqualitiesAssessmentSavedEPRInWorkplacesFolder { get; set; }
        
        public bool? FsgPreOpeningMilestonesSeenEvidenceOfAcceptedOffers { get; set; }

        public string FsgPreOpeningMilestonesAcceptedOffersComments { get; set; }

        public bool? FsgPreOpeningMilestonesAcceptedOffersEmailSavedToWorkplaces { get; set; }
    }
}
