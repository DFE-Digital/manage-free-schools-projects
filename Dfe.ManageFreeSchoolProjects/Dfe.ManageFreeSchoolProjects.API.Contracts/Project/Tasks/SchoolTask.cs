using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record SchoolTask
    {
        public string CurrentFreeSchoolName { get; set; }
        public SchoolType SchoolType { get; set; }
        public SchoolPhase SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public string Nursery { get; set; }
        public Gender Gender { get; set; }
        public string SixthForm { get; set; }

        public FaithStatus FaithStatus { get; set; }

        public FaithType FaithType { get; set; }

        public string OtherFaithType { get; set; }
        
        public bool MarkedAsComplete { get; set; }
    }

    public enum SchoolType
    {
        NotSet = 0,
        [Description("Alternative provision")]
        AlternativeProvision = 1, 
        [Description("Further education")]
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
        [Description("Not set")]
        NotSet,
        [Description("Primary")]
        Primary,
        [Description("Secondary")]
        Secondary,
        [Description("16 to 19")]
        SixteenToNineteen, 
        [Description("All-through")]
        AllThrough, 
    }

    public enum FaithType
    {
        NotSet,
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
        NotSet,
        None,
        Designation,
        Ethos,
    }

    public enum Gender
    {
        NotSet,
        [Description("Boys only")]
        BoysOnly,
        [Description("Girls only")]
        GirlsOnly,
        [Description("Mixed")]
        Mixed
    }
}
