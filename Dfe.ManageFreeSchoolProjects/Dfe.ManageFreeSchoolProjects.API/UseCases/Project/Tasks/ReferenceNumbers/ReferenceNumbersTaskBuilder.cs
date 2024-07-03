using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReferenceNumbers
{
    public static class ReferenceNumbersTaskBuilder
    {
        public static ReferenceNumbersTask Build(Kpi kpi)
        {
            if (kpi == null)
            {
                return new ReferenceNumbersTask();
            }

            return new ReferenceNumbersTask()
            {
                ProjectId = kpi.ProjectStatusProjectId,
                Urn = kpi.ProjectStatusUrnWhenGivenOne,
                
            };

        }
    }
}
