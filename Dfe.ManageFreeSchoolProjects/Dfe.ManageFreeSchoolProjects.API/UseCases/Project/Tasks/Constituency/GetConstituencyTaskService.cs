using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency
{
    public class GetConstituencyTaskService : IGetTaskService
    {
        public Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var query = parameters.BaseQuery;

            var result = query.Select(kpi => new GetProjectByTaskResponse()
            {
                Constituency = ConstituencyTaskMapper.Map(kpi)
            }).FirstOrDefaultAsync();

            return result;
        }
    }
}
