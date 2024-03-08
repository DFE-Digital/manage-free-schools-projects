﻿using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class GetProjectByTaskResponse
    {
        public string SchoolName { get; set; }
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public TrustTask Trust { get; set; }
        public ConstituencyTask Constituency { get; set; }
        public RegionAndLocalAuthorityTask RegionAndLocalAuthority { get; set; }
        public RiskAppraisalMeetingTask RiskAppraisalMeeting { get; set; }

        public KickOffMeetingTask KickOffMeeting { get; set; }

        public ModelFundingAgreementTask ModelFundingAgreement{ get; set; }

        public StatutoryConsultationTask StatutoryConsultation { get; set; }
        public ArticlesOfAssociationTask ArticlesOfAssociation { get; set; }

        public GiasTask Gias { get; set; }
        public FinancePlanTask FinancePlan { get; set; }

        public DraftGovernancePlanTask DraftGovernancePlan { get; set; }
        
        public EducationBriefTask EducationBrief { get; set; }

        public AdmissionsArrangementsTask AdmissionsArrangements { get; set; }
    }
}
