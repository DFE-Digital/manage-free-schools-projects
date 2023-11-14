using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public enum SchoolType
    {
        [Description("Alternative position")]
        AlternativePosition = 1,
        [Description("Mainstream")]
        MainStream = 2,
        [Description("Special")]
        Special = 3,
        [Description("Studio school")]
        StudioSchool = 4,
        [Description("University technical college")]
        UniversityTechnicalCollege = 5,
    }
}
