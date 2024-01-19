using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts
{
    public class GetTrustTaskService : IGetTaskService
    {
        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var query = parameters.BaseQuery;

            var result = await query.Select(kpi => new GetProjectByTaskResponse()
            {
                Trust = new()
                {
                    TRN = kpi.TrustId,
                    TrustName = kpi.TrustName,
                    TrustType = ProjectMapper.ToTrustType(kpi.TrustType)
                }
            }).FirstOrDefaultAsync();

            return result;
        }
    }
}
