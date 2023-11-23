using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public enum SchoolType
    {
        NotSet = 0,
        [Description("Alternative provision")]
        AlternativeProvision = 1,
        [Description("Mainstream")]
        Mainstream = 2,
        [Description("Special")]
        Special = 3,
        [Description("Studio school")]
        StudioSchool = 4,
        [Description("University technical college")]
        UniversityTechnicalCollege = 5
    }
}
