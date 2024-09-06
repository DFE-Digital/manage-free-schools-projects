using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class UpdateProjectByTaskRequest
    {
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public ReferenceNumbersTask ReferenceNumbers { get; set; }
        public TrustTask Trust { get; set; }
        public RegionAndLocalAuthorityTask RegionAndLocalAuthorityTask { get; set; }
        public RiskAppraisalMeetingTask RiskAppraisalMeeting { get; set; }
        public ConstituencyTask Constituency { get; set; }

        public KickOffMeetingTask KickOffMeeting { get; set; }

        public GiasTask Gias { get; set; }
        public FundingAgreementTask FundingAgreement { get; set; }

        public StatutoryConsultationTask StatutoryConsultation { get; set; }

        public ArticlesOfAssociationTask ArticlesOfAssociation { get; set; }

        public FinancePlanTask FinancePlan { get; set; }

        public DraftGovernancePlanTask DraftGovernancePlan { get; set; }

        public EducationBriefTask EducationBrief { get; set; }

        public AdmissionsArrangementsTask AdmissionsArrangements { get; set; }

        public ImpactAssessmentTask ImpactAssessment { get; set; }

        public EvidenceOfAcceptedOffersTask EvidenceOfAcceptedOffers { get; set; }

        public EqualitiesAssessmentTask EqualitiesAssessment { get; set; }

        public OfstedInspectionTask OfstedInspection { get; set; }

        public ApplicationsEvidenceTask ApplicationsEvidence { get; set; }

        public FundingAgreementHealthCheckTask FundingAgreementHealthCheck { get; set; }
        public FundingAgreementSubmissionTask FundingAgreementSubmission { get; set; }
        public PaymentScheduleTask PaymentSchedule { get; set; }

        public TrustPDGLetterSentTask TrustPDGLetterSent { get; set; }

        public StopPaymentTask StopPayment { get; set; }

        public RefundsTask Refunds { get; set; }

        public WriteOffTask WriteOff { get; set; }
        public FinalFinancePlanTask FinalFinancePlan { get; set; }

        public PupilNumbersChecksTask PupilNumbersChecks { get; set; }

        public CommissionedExternalExpertTask CommissionedExternalExpert { get; set; }

        public MovingToOpenTask MovingToOpen { get; set; }

        public PrincipalDesignateTask PrincipalDesignate { get; set; }

        public DueDiligenceChecks DueDiligenceChecks { get; set; }
        
        public string TaskToUpdate => TaskMappings.Where(kvp => kvp is { Key: not null, Value: not null })
            .Select(kvp => kvp.Value).FirstOrDefault();
        
        private Dictionary<object, string> TaskMappings => new()
        {
            { School, "School" },
            { ReferenceNumbers, TaskName.ReferenceNumbers.ToString() },
            { Dates, "Dates" },
            { Trust, "Trust" },
            { RegionAndLocalAuthorityTask, "RegionAndLocalAuthority" },
            { RiskAppraisalMeeting, "RiskAppraisalMeeting" },
            { Constituency, "Constituency" },
            { ArticlesOfAssociation, TaskName.ArticlesOfAssociation.ToString() },
            { FinancePlan, TaskName.FinancePlan.ToString() },
            { KickOffMeeting, TaskName.KickOffMeeting.ToString() },
            { FundingAgreement, TaskName.FundingAgreement.ToString() },
            { DraftGovernancePlan, TaskName.DraftGovernancePlan.ToString() },
            { Gias, TaskName.Gias.ToString() },
            { StatutoryConsultation, TaskName.StatutoryConsultation.ToString() },
            { EducationBrief, TaskName.EducationBrief.ToString() },
            { AdmissionsArrangements, TaskName.AdmissionsArrangements.ToString() },
            { EqualitiesAssessment, TaskName.EqualitiesAssessment.ToString() },
            { ImpactAssessment, TaskName.ImpactAssessment.ToString() },
            { EvidenceOfAcceptedOffers, TaskName.EvidenceOfAcceptedOffers.ToString() },
            { OfstedInspection, TaskName.OfstedInspection.ToString() },
            { ApplicationsEvidence, TaskName.ApplicationsEvidence.ToString() },
            { FundingAgreementHealthCheck, TaskName.FundingAgreementHealthCheck.ToString() },
            { FundingAgreementSubmission, TaskName.FundingAgreementSubmission.ToString() },
            { PaymentSchedule, TaskName.PaymentSchedule.ToString() },
            { TrustPDGLetterSent, TaskName.TrustPDGLetterSent.ToString() },
            { StopPayment, TaskName.StopPayment.ToString() },
            { Refunds, TaskName.Refunds.ToString() },
            { WriteOff, TaskName.WriteOff.ToString() },
            { FinalFinancePlan, TaskName.FinalFinancePlan.ToString() },
            { PupilNumbersChecks, TaskName.PupilNumbersChecks.ToString() },
            { CommissionedExternalExpert, TaskName.CommissionedExternalExpert.ToString() },
            { MovingToOpen, TaskName.MovingToOpen.ToString() },
            { PrincipalDesignate, TaskName.PrincipalDesignate.ToString() },
            { DueDiligenceChecks, TaskName.DueDiligenceChecks.ToString() }
        };
    }
}