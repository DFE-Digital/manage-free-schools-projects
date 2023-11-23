using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using SchoolType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.SchoolType;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{
    public static class ProjectMapper
    {
        public static SchoolType? ToSchoolType(string schoolType)
        {
            return schoolType switch
            {
                "FS - AP" => SchoolType.AlternativeProvision,
                "FS - Special" => SchoolType.Special,
                "SS" => SchoolType.StudioSchool,
                "UTC" => SchoolType.UniversityTechnicalCollege,
                _ => SchoolType.NotSet
            };
        }

        public static string ToSchoolType(SchoolType? schoolType)
        {
            return schoolType switch
            {
                SchoolType.AlternativeProvision => "FS - AP",
                SchoolType.Special => "FS - Special",
                SchoolType.StudioSchool => "SS",
                SchoolType.UniversityTechnicalCollege => "UTC",
                _ => string.Empty
            };
        }

        public static SchoolPhase ToSchoolPhase(string schoolPhase)
        {
            return schoolPhase switch
            {
                "Primary" => SchoolPhase.Primary,
                "Secondary" => SchoolPhase.Secondary,
                "16 to 19" => SchoolPhase.SixteenToNineteen,
                "All-through" => SchoolPhase.AllThrough,
                _ => SchoolPhase.NotSet
            };
        }

        public static string ToSchoolPhase(SchoolPhase schoolPhase)
        {
            return schoolPhase switch
            {
                SchoolPhase.Primary => "Primary",
                SchoolPhase.Secondary => "Secondary",
                SchoolPhase.SixteenToNineteen => "16 to 19",
                SchoolPhase.AllThrough => "All-through",
                _ => "Not set"
            };
        }
    }
}
