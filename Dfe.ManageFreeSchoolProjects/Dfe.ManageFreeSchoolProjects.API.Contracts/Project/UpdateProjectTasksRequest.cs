namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public class UpdateProjectTasksRequest
    {
        public ProjectTaskRequest Tasks { get; set; }
    }

    public record ProjectTaskRequest
    {
        public RiskAppraisalTaskRequest RiskAppraisal { get; set; }
        public DatesTaskRequest Dates { get; set; }
        public SchoolTaskRequest School { get; set; }
        public ConstructionTaskRequest Construction { get; set; }
    }

    public record SchoolTaskRequest
    {
        public string SchoolType { get; set; }
        public string SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public string Nursery { get; set; }
        public string SixthForm { get; set; }
        public string CompanyName { get; set; }
        public string NumberOfCompanyMembers { get; set; }
        public string ProposedChairOfTrustees { get; set; }
    }

    public record ConstructionTaskRequest
    {
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

    public record RiskAppraisalTaskRequest
    {
        public string SharepointLink { get; set; }
        public string EducationRiskRating { get; set; }
        public string GovernanceRiskRating { get; set; }
        public string FinanceRiskRating { get; set; }
        public bool MarkedAsComplete { get; set; }
    }

    public record DatesTaskRequest
    {
        public string DateOfEntryIntoPreopening { get; set; }

        public string RealisticYearOfOpening { get; set; }

        public string ProvisionalOpeningDateAgreedWithTrust { get; set; }

        public string ActualOpeningDate { get; set; }

        public string OpeningAcademicYear { get; set; }

        public string StartOfTermDate { get; set; }

        public string ProvisionalKickoffMeetingDate { get; set; }

        public string ActualKickOffMeetingDate { get; set; }
    }
}
