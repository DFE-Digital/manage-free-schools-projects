using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

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
    }
}
