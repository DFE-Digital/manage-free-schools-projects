using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.AdmissionsArrangements
{
    public class AdmissionsArrangementsTaskBuilder
    {
        public static AdmissionsArrangementsTask Build()
        {
            return new AdmissionsArrangementsTask();
        }
    }
}
