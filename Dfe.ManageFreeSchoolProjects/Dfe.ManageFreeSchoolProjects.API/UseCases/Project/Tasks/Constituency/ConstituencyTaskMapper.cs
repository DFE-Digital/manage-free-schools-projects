using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency
{
    public static class ConstituencyTaskMapper
    {
        public static ConstituencyTask Map(Kpi kpi)
        {
            return new ConstituencyTask()
            {
                Name = kpi.SchoolDetailsConstituency,
                MPName = kpi.SchoolDetailsConstituencyMp,
                Party = kpi.SchoolDetailsPoliticalParty,
            };
        }
    }
}
