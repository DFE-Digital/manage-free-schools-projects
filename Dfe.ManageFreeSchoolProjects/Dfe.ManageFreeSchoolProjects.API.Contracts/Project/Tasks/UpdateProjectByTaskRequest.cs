namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class UpdateProjectByTaskRequest
    {
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public TrustTask Trust { get; set; }
        public RegionAndLocalAuthorityTask RegionAndLocalAuthorityTask { get; set; }
        public RiskAppraisalMeetingTask RiskAppraisalMeeting { get; set; }
		public ConstituencyTask Constituency { get; set; }
        
        public KickOffMeetingTask KickOffMeeting { get; set; }
        
        public GiasTask Gias { get; set; }
        public ModelFundingAgreementTask ModelFundingAgreement { get; set; }

        public StatutoryConsultationTask StatutoryConsultation { get; set; }

        public ArticlesOfAssociationTask ArticlesOfAssociation { get; set; }

        public FinancePlanTask FinancePlan { get; set; }

        public DraftGovernancePlanTask DraftGovernancePlan { get;set;}
        
        public EducationBriefTask EducationBrief { get; set; }

        public AdmissionsArrangementsTask AdmissionsArrangements { get; set; }
        
        public ImpactAssessmentTask ImpactAssessment { get; set; }

        public string TaskToUpdate
        {
            get
            {
                if (School != null)
                    return "School";
                if (Dates != null)
                    return "Dates";
                if (Trust != null)
                    return "Trust";
                if (RegionAndLocalAuthorityTask != null)
                    return "RegionAndLocalAuthority";
                if (RiskAppraisalMeeting != null)
                    return "RiskAppraisalMeeting";
				if (Constituency != null)
					return "Constituency";
                if (ArticlesOfAssociation != null)
                    return TaskName.ArticlesOfAssociation.ToString();
                if (FinancePlan != null)
                    return TaskName.FinancePlan.ToString();
                if (KickOffMeeting != null)
                    return TaskName.KickOffMeeting.ToString();
                if (ModelFundingAgreement != null)
                    return TaskName.ModelFundingAgreement.ToString();
                if (DraftGovernancePlan != null)
                    return TaskName.DraftGovernancePlan.ToString();
                if (Gias != null)
                    return TaskName.Gias.ToString();
                if (StatutoryConsultation != null)
                    return TaskName.StatutoryConsultation.ToString();
                if (EducationBrief != null)
                    return TaskName.EducationBrief.ToString();
                if (AdmissionsArrangements != null)
                    return TaskName.AdmissionsArrangements.ToString();
                if (ImpactAssessment != null)
                    return TaskName.ImpactAssessment.ToString();
                return null;
            }
        }
    }
}
