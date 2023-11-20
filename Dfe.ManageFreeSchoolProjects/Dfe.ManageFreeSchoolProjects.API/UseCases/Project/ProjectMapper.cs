using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using SchoolType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.SchoolType;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{
    public static class ProjectMapper
    {
        public static SchoolType? ToSchoolType(string schoolType)
        {
            if (schoolType == "FS - AP")
            {
                return SchoolType.AlternativePosition;
            }

            if (schoolType == "FS - Special")
            {
                return SchoolType.Special;
            }

            if (schoolType == "SS")
            {
                return SchoolType.StudioSchool;
            }

            if (schoolType == "UTC")
            {
                return SchoolType.UniversityTechnicalCollege;
            }

            return null;
        }

        public static string ToSchoolType(SchoolType? schoolType)
        {
            if (schoolType == SchoolType.AlternativePosition)
            {
                return "FS - AP";
            }

            if (schoolType == SchoolType.Special)
            {
                return "FS - Special";
            }

            if (schoolType == SchoolType.StudioSchool)
            {
                return "SS";
            }

            if (schoolType == SchoolType.UniversityTechnicalCollege)
            {
                return "UTC";
            }

            return null;
        }

        public static SchoolPhase? ToSchoolPhase(string schoolPhase)
        {
            if (schoolPhase == "Not set")
            {
                return SchoolPhase.NotSet;
            }

            if (schoolPhase == "Primary")
            {
                return SchoolPhase.Primary;
            }

            if (schoolPhase == "Secondary")
            {
                return SchoolPhase.Secondary;
            }

            if (schoolPhase == "16 to 19")
            {
                return SchoolPhase.SixteenToNineteen;
            }

            if (schoolPhase == "All-through")
            {
                return SchoolPhase.SixteenToNineteen;
            }

            return null;
        }

        public static string ToSchoolPhase(SchoolPhase? schoolPhase)
        {
            if (schoolPhase == SchoolPhase.NotSet)
            {
                return "Not set";
            }

            if (schoolPhase == SchoolPhase.Primary)
            {
                return "Primary";
            }

            if (schoolPhase == SchoolPhase.Secondary)
            {
                return "Secondary";
            }

            if (schoolPhase == SchoolPhase.SixteenToNineteen)
            {
                return "16 to 19";
            }

            if (schoolPhase == SchoolPhase.AllThrough)
            {
                return "All-through";
            }

            return null;
        }
    }
}
