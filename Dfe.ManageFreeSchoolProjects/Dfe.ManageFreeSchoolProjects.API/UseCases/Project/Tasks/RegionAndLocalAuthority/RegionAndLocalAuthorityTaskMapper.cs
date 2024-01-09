using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RegionAndLocalAuthority
{
    public static class RegionAndLocalAuthorityTaskMapper
    {
        public static RegionAndLocalAuthorityTask Map(Kpi kpi)
        {
            return new RegionAndLocalAuthorityTask
            {
                Region = kpi.SchoolDetailsGeographicalRegion,
                LocalAuthority = kpi.LocalAuthority
            };
        }
    }
}
