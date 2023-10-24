using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record SchoolTask
    {
        public string CurrentFreeSchoolName { get; set; }
        public SchoolType? SchoolType { get; set; }
        public SchoolPhase? SchoolPhase { get; set; }
        public string AgeRange { get; set; }
        public Nursery? Nursery { get; set; }
        public Gender Gender { get; set; }
        public SixthForm? SixthForm { get; set; }

        public FaithStatus FaithStatus { get; set; }

        public FaithType FaithType { get; set; }

        public string OtherFaithType { get; set; }
        
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

    public enum Nursery
    {
        Yes,
        No
    }

    public enum SixthForm
    {
        Yes,
        No
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

    public enum Gender
    {
        [Description("Boys only")]
        BoysOnly,
        [Description("Girls only")]
        GirlsOnly,
        [Description("Mixed")]
        Mixed
    }
}
