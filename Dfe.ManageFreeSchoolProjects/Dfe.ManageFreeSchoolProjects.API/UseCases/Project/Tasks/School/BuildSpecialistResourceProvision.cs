using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School
{
    public class BuildSpecialistResourceProvision
    {
        public static string GetLegacySpecialistResourceProvision(ClassType.SpecialEducationNeeds specialEducationNeeds,
                                                               ClassType.AlternativeProvision alternativeProvision)
        {
            if (specialEducationNeeds == ClassType.SpecialEducationNeeds.Yes ||
                alternativeProvision == ClassType.AlternativeProvision.Yes)
            {
                return "Yes";
            }

            if (specialEducationNeeds == ClassType.SpecialEducationNeeds.NotSet &&
                alternativeProvision == ClassType.AlternativeProvision.NotSet)
            {
                return null;
            }

            return "No";
        }
    }
}
