using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record SchoolTask
    {
        public string CurrentFreeSchoolName { get; set; }
        public string SchoolType { get; set; }
        public string SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public string Nursery { get; set; }
        public string SixthForm { get; set; }
        public string CompanyName { get; set; }
        public string NumberOfCompanyMembers { get; set; }
        public string ProposedChairOfTrustees { get; set; }
        
        public bool MarkedAsComplete { get; set; }
    }

    public enum SchoolType
    {
        [Description("Alternative provision")]
        AlternativeProvision = 1, 
        [Description("Further edcuation")]
        FurtherEducation = 2,
        [Description("Mainstream")]
        Mainstream = 3, 
        [Description("Special")]
        Special = 4, 
        [Description("Studio school")]
        StudioSchool = 5, 
        [Description("University technical college")]
        UniversityTechnicalCollege = 6
    }

    public enum SchoolPhase
    {
        [Description("Primary")]
        Primary = 1,
        [Description("Secondary")]
        Secondary = 2,
        [Description("16 to 19")]
        SixteenToNineteen = 3, 
        [Description("All-Through")]
        AllThrough = 4, 
    }

    public enum FaithType
    {
        [Description("Church of England")]
        ChurchOfEngland,
        [Description("Christian")]
        Christian,
        [Description("Greek Orthodox")]
        GreekOrthodox,
        [Description("Hindu")]
        Hindu,
        [Description("Jewish")]
        Jewish,
        [Description("Methodist")]
        Methodist,
        [Description("Muslim")]
        Muslim,
        [Description("Roman Catholic")]
        RomanCatholic,
        [Description("Sikh")]
        Sikh,
        [Description("Other")]
        Other
    }

    public enum FaithStatus
    {
        Designation,
        Ethos,
        None
    }
}
