namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public record ProjectOverviewResponse
    {
        public ProjectStatusResponse ProjectStatus { get; set; }
        public SchoolDetailsResponse SchoolDetails { get; set; }
        public KeyContactsResponse KeyContacts { get; set; }
        public SiteInformationResponse SiteInformation { get; set; }
        public PupilInformationResponse PupilInformation { get; set; }
        public RagRatingResponse RagRating { get; set; }
    }

    public record ProjectStatusResponse
    {
        public string CurrentFreeSchoolName { get; set; }
        public string ProjectStatus { get; set; }
        public string FreeSchoolsApplicationNumber { get; set; }
        public string ProjectId { get; set; }
        public string Urn { get; set; }
        public string ApplicationWave { get; set; }
        public string RealisticYearOfOpening { get; set; }
        public string DateOfEntryIntoPreopening { get; set; }
        public string ProvisionalOpeningDateAgreedWithTrust { get; set; }
        public string ActualOpeningDate { get; set; }
        public string OpeningAcademicYear { get; set; }
    }

    public record SchoolDetailsResponse
    {
        public string LocalAuthority { get; set; }
        public string Region { get; set; }
        public string Constituency { get; set; }
        public string ConstituencyMp { get; set; }
        public string NumberOfEntryForms { get; set; }
        public string SchoolType { get; set; }
        public string SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public string Gender { get; set; }
        public string Nursery { get; set; }
        public string SixthForm { get; set; }
        public string IndependentConverter { get; set; }
        public string SpecialistResourceProvision { get; set; }
        public string FaithStatus { get; set; }
        public string FaithType { get; set; }
        public string TrustId { get; set; }
        public string TrustName { get; set; }
        public string TrustType { get; set; }
    }

    public record KeyContactsResponse
    {
        public string LeadContact { get; set; }
        public string TeamLeader { get; set; }
        public string Grade6 { get; set; }
        public string ProjectDirector { get; set; }
    }

    public record SiteInformationResponse
    {
        public string Property { get; set; }
        public string Postcode { get; set; }
    }

    public record PupilInformationResponse
    {
    }

    public record RagRatingResponse
    {
    }
}
