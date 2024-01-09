using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts
{
    public static class TrustTaskMapper
    {
        public static TrustTask Map(Kpi kpi)
        {
            return new TrustTask()
            {
                TRN = kpi.TrustId,
                TrustName = kpi.TrustName,
                TrustType = kpi.TrustType
            };
        }
    }
}
